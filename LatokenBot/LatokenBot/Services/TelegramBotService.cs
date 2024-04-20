using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Microsoft.SemanticKernel;
using LatokenBot.Extensions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace LatokenBot.Services;

// IServiceScopeFactory _serviceScopeFactory,
internal class TelegramBotService(ITelegramBotClient _telegramBotClient, IChatCompletionService _chatCompletionService, IOptions<ReceiverOptions> _receiverOptions, CancellationTokenService _cancellationTokenService)
{
    private readonly Dictionary<long, ChatHistory> _usersHistory = [];

    private const string _promptTemplate = @"
ПРИМЕР ТВОЕГО ОТВЕТА:
1) ИСПОЛЬЗУЙ БОЛЬШЕ СМАЙЛИКОВ (Например: 💙🌀🎐🧿💤🌑💖🐇✨️💀😁🔥 и тд) 
2) НЕ ЗАБЫВАЙ ПРО ССЫЛКИ В ТЕКСТЕ
3) НЕ ЗАБЫВАЙ ПРО ВОПРОС, КОТОРЫЙ ТЫ ДОЛЖЕН ЗАДАТЬ (из test.txt) В КОНЦЕ ТЕКСТА
4) ГЕНЕРИРУЙ РАЗНЫЕ ОТВЕТЫ НА ОСНОВЕ ТВОЕГО ОБУЧЕНИЯ
5) ВМЕСТО ** ИСПОЛЬЗУЙ HTML теги <b></b>

Привет! 👋 Как я могу помочь вам сегодня? Если вы здесь для участия в хакатоне AI Web3 от Latoken, пожалуйста, дайте мне знать, и я предоставлю вам всю необходимую информацию.

ИНФОРМАЦИЯ ДЛЯ GPT, ОСНОВАНА НА: 

<b>Если вы хотите узнать, как принять участие в хакатоне AI Web3 от Latoken, выполните следующие шаги:</b>

Не забудьте ознакомиться с дополнительной информацией о хакатоне: [Подробнее о хакатоне] (ссылка, основана на содержимом about.txt)

1. <b>Регистрация на хакатон:</b>
   - Пожалуйста, перейдите по [ссылке для регистрации] (ссылка и информация, основана на содержимом hackaton.txt).

2. <b>Подготовка:</b>
   - Пройдите тест по ссылке [Пройти тест] (ссылка и информация, основана на содержимом test.txt), чтобы проверить свои знания и подготовиться к хакатону.

3. <b>Участие в хакатоне:</b>
   - (информация, основана на содержимом hackathon.txt)

4. <b>Работа в команде:</b>
   - Во время хакатона вам нужно будет работать в команде, чтобы решить поставленную задачу, используя свои знания и навыки в области AI и Web3.
   - (информация, основана на содержимом about.txt)

5. <b>Принципы Латокена:</b>
   - (информация, основана на содержимом hard.txt)

**ВЗЯТЬ 1 ВОПРОС ИЗ ФАЙЛА test.txt:**

<b>Если у вас есть дополнительные вопросы или нужна помощь, не стесняйтесь спрашивать. Удачи на хакатоне!</b>";

    private Kernel? _kernel;

    public void Start(Kernel kernel)
    {
        _kernel = kernel;

        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            _receiverOptions.Value,
            _cancellationTokenService.Token
        );
    }

    // using var serviceScope = _serviceScopeFactory.CreateScope();
    // var serviceProvider = serviceScope.ServiceProvider;
    private async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update, CancellationToken _cancellationToken)
    {
        if (update?.Message?.From?.Id is not { } chatId)
            return; 

        if (update.Message.Text is not { } messageText)
            return;
         
        try
        {
            if (!_usersHistory.TryGetValue(chatId, out var chatHistory))
            {
                chatHistory = [];
                _usersHistory.Add(chatId, chatHistory);
            }

            Console.WriteLine($"{chatId} > {messageText}");

            //// Создание истории чата для Semantic Kernel 
            chatHistory.AddUserMessage(messageText);

            var chatContent = TextFileExtension.GetTraining();
            var fullPrompt = $"{_promptTemplate}\n{chatContent}";

            // GetStreamingChatMessageContentsAsync(); - получение ответа по символьно
            var result = await _chatCompletionService.GetChatMessageContentAsync(
                chatHistory,
                executionSettings: new OpenAIPromptExecutionSettings()
                {
                    ChatSystemPrompt = fullPrompt,
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                    Temperature = 0
                }, kernel: _kernel, cancellationToken: _cancellationToken);

            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, result.ToString(), parseMode: ParseMode.Html, cancellationToken: _cancellationToken);

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