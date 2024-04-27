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
    private string[] _promptTemplate;
    private ChatHistory _chatHistory;
    private Kernel? _kernel;

    public void Start(Kernel kernel)
    {
        // оставить rules и добавить обращения к бд (rag-агент)
        _promptTemplate = TextFileExtension.GetTrainingDocuments([
            @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\rules.txt",
            @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\about.txt",
            @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\hackaton.txt",
            @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\test.txt",
            @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\hard.txt",
        ]);
        _chatHistory = [];
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
            if (update?.Message?.From?.Id is not { } chatId)
                return;

            if (update.Message.Text is not { } messageText)
                return;

            var user = await _userService.GetUserByTelegramId(chatId)
                ?? await _userService.AddAndGetUser(chatId, update.Message.From.FirstName, update.Message.From.LastName);

            Console.WriteLine($"{chatId} > {messageText}");

            _chatHistory.AddUserMessage(messageText);

            user.ChatHistory.Add(new() { Message = messageText, TimeMessage = DateTime.Now });
            await _userService.SaveUser(user);

            //await _documentService.InitializationAbout();
            //await _documentService.InitializationHackaton();
            //await _documentService.InitializationTest();
            //await _documentService.InitializationHard();

            // GetStreamingChatMessageContentsAsync(); - получение ответа по символьно
            var result = await _chatCompletionService.GetChatMessageContentAsync(
                _chatHistory,
                executionSettings: new OpenAIPromptExecutionSettings()
                {
                    ChatSystemPrompt = $"{_promptTemplate}",
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                    Temperature = 0
                }, kernel: _kernel, cancellationToken: _cancellationToken);

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: result.ToString(),
                parseMode: ParseMode.Html,
                cancellationToken: _cancellationToken
                );

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