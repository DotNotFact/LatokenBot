using Microsoft.Extensions.DependencyInjection;
using LatokenBot.Services.Actions;
using Microsoft.SemanticKernel;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using LatokenBot.Services;
using LatokenBot.Plugins;
using Telegram.Bot;

// Обучение на основе 4 txt файлов в переменной prompt
// Junior: Боту можно написать вопросы и получить ответы на основе материалов Хакатона.
// Middle: Junior + Cделать группу и добавить туда бота. Бот будет отвечать на задаваемые вопросы по теме хакатона в группу, бот должен на них ответить в группе. 
// Senior: Middle + Бот, ответив на вопрос, задает вопросы тестируя кандидата на основе Хакатон теста. 

var builder = Kernel.CreateBuilder();
var services = builder.Services;

string telegramKey = Environment.GetEnvironmentVariable("telegram_api_key");
string apiKey = Environment.GetEnvironmentVariable("chat_gpt_api_key");
string modelId = "gpt-4-1106-preview";

// Инициализация настроек для чата
builder.AddOpenAIChatCompletion(modelId: modelId, apiKey: apiKey);

// Добавление плагинов
//builder.Plugins
//    .AddFromType<TextAnalysisPlugin>();

// Добавление сервисов
services
    .AddSingleton<ITelegramBotClient>(s => new TelegramBotClient(telegramKey))
    .AddSingleton<CancellationTokenService>()
    .AddSingleton<TelegramBotService>()
    .AddScoped<StartService>();

services.Configure<ReceiverOptions>(s =>
{
    s.AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery];
});

var kernel = builder.Build();

var telegramBotService = kernel.GetRequiredService<TelegramBotService>();

telegramBotService.Start(kernel);

Console.WriteLine("Телеграм бот начал свою работу. Нажмите <Enter> для выхода.");
Console.ReadLine();