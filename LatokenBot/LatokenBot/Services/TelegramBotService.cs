using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.Options;
using LatokenBot.Services.Actions;
using Telegram.Bot.Types.Enums;
using Microsoft.SemanticKernel;
using LatokenBot.Extensions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace LatokenBot.Services;

internal class TelegramBotService(UserService _userService, DocumentService _documentService, ITelegramBotClient _telegramBotClient, IChatCompletionService _chatCompletionService, IOptions<ReceiverOptions> _receiverOptions, CancellationTokenService _cancellationTokenService)
{
    private Dictionary<long, ChatHistory> _chatHistories;

    private string _promptTemplate;
    private Kernel? _kernel;

    public void Start(Kernel kernel)
    {
        // оставить rules и добавить обращения к бд (rag-агент)
        _promptTemplate = TextFileExtension.GetTraining();
        _chatHistories = [];
        _kernel = kernel;

        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            _receiverOptions.Value,
            _cancellationTokenService.Token
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update, CancellationToken _cancellationToken)
    {
        try
        {
            if (update?.Message?.From?.Id is not { } userId)
                return;

            if (update.Message.Text is not { } messageText)
                return;

            var user = await _userService.AddAndGetUserByTelegramId(userId, update.Message.From.FirstName, update.Message.From.LastName);

            if (!_chatHistories.TryGetValue(userId, out var _chatHistory))
            {
                _chatHistory = [];

                foreach (var chatHistory in user.ChatHistory)
                    _chatHistory.Add(new ChatMessageContent(AuthorRole.User, chatHistory.Message));

                _chatHistories.Add(userId, _chatHistory);
            }

            Console.WriteLine($"{userId} > {messageText}");

            _chatHistory.AddUserMessage(messageText);

            user.ChatHistory.Add(new() { Message = messageText, TimeMessage = DateTime.Now });
            await _userService.SaveUser(user);

            // await _documentService.InitializationAbout();
            // await _documentService.InitializationHackaton();
            // await _documentService.InitializationTest();
            // await _documentService.InitializationHard();

            var message = await _botClient.SendTextMessageAsync(
                update.Message.Chat.Id,
                "Идёт генерация ответа....",
                cancellationToken: _cancellationToken);

            await _botClient.SendChatActionAsync(
                update.Message.Chat.Id,
                ChatAction.Typing,
                cancellationToken: _cancellationToken);

            var result = _chatCompletionService.GetStreamingChatMessageContentsAsync(
                _chatHistory,
                executionSettings: new OpenAIPromptExecutionSettings()
                {
                    ChatSystemPrompt = $"{_promptTemplate}",
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                    Temperature = 0
                }, kernel: _kernel, cancellationToken: _cancellationToken);

            string fullMessage = "";
            string chunk = "";

            await foreach (var content in result)
            {
                chunk += content.Content;

                if (chunk.Length < 100)
                    continue;

                fullMessage += chunk;
                chunk = "";

                await _botClient.SendChatActionAsync(
                    update.Message.Chat.Id,
                    ChatAction.Typing,
                    cancellationToken: _cancellationToken
                    );

                await _botClient.EditMessageTextAsync(
                    update.Message.Chat.Id,
                    message.MessageId,
                    fullMessage,
                    parseMode: ParseMode.Html,
                    cancellationToken: _cancellationToken
                    );
            }

            if (chunk.Length > 0)
            {
                fullMessage += chunk;

                await _botClient.SendChatActionAsync(
                    update.Message.Chat.Id,
                    ChatAction.Typing,
                    cancellationToken: _cancellationToken
                    );

                await _botClient.EditMessageTextAsync(
                    update.Message.Chat.Id,
                    message.MessageId,
                    fullMessage,
                    parseMode: ParseMode.Html,
                    cancellationToken: _cancellationToken
                    );
            }


            // GetStreamingChatMessageContentsAsync(); - получение ответа по символьно
            //var result = await _chatCompletionService.GetChatMessageContentAsync(
            //    _chatHistory,
            //    executionSettings: new OpenAIPromptExecutionSettings()
            //    {
            //        ChatSystemPrompt = $"{_promptTemplate}",
            //        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            //        Temperature = 0
            //    }, kernel: _kernel, cancellationToken: _cancellationToken);

            //await _botClient.SendTextMessageAsync(
            //    chatId: chatId,
            //    text: result.ToString(),
            //    parseMode: ParseMode.Html,
            //    cancellationToken: _cancellationToken
            //    );

            // chatMessages.AddAssistantMessage(fullMessage);

            // Очистка истории чата, если нужно начинать "с чистого листа" после каждого запроса
            // ChatHistory.Clear();
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient _botClient, Exception exception, CancellationToken _cancellationToken)
    {
        Console.WriteLine($"Произошла ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}