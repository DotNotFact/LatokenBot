using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory.AI.OpenAI;
using Microsoft.Extensions.Logging;
using LatokenBot.Services.Actions;
using Microsoft.KernelMemory.AI;
using Microsoft.SemanticKernel;
using Telegram.Bot.Types.Enums;
using Microsoft.KernelMemory;
using Telegram.Bot.Polling;
using LatokenBot.Services;
using LatokenBot.Plugins;
using LatokenBot.Data;
using MongoDB.Driver;
using Telegram.Bot;

// Обучение на основе 4 txt файлов в переменной prompt
// Junior: Боту можно написать вопросы и получить ответы на основе материалов Хакатона.
// Middle: Junior + Cделать группу и добавить туда бота. Бот будет отвечать на задаваемые вопросы по теме хакатона в группу, бот должен на них ответить в группе. 
// Senior: Middle + Бот, ответив на вопрос, задает вопросы тестируя кандидата на основе Хакатон теста. 

// Инициализация строителя
var builder = Kernel.CreateBuilder();
var services = builder.Services;
var plugins = builder.Plugins;

// Константы
string telegramKey = Environment.GetEnvironmentVariable("telegram_api_key") ?? throw new ArgumentNullException(nameof(telegramKey));
string apiKey = Environment.GetEnvironmentVariable("chat_gpt_api_key") ?? throw new ArgumentNullException(nameof(apiKey));
const string connectionString = "mongodb://localhost:27017";
const string embeddingModel = "text-embedding-ada-002";
const string modelId = "gpt-4-1106-preview";

// Валидация конфигурации
var openAIConfig = new OpenAIConfig
{
    EmbeddingModel = embeddingModel,
    TextModel = modelId,
    APIKey = apiKey,
};
openAIConfig.Validate();

// Плагины
plugins
    .AddFromType<MongoDBPlugin>();

// Сервисы
services
    .AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(connectionString))
    .AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(telegramKey))
    .AddSingleton<CancellationTokenService>()
    .AddSingleton<TelegramBotService>()
    .AddScoped<DataRetrievalService>()
    .AddScoped<DocumentService>()
    .AddScoped<MongoDatabase>()
    .AddScoped<UserService>()
    // Регистрация OpenAITextEmbeddingGenerator с использованием конфигурации
    .AddScoped<ITextEmbeddingGenerator>(sp =>
    {
        var httpClientFactory = sp.GetService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();

        return new OpenAITextEmbeddingGenerator(openAIConfig, null, sp.GetService<ILogger<OpenAITextEmbeddingGenerator>>(), httpClient);
    });

// Добавляем IHttpClientFactory в DI контейнер
services.AddHttpClient();

// Конфигурация сообщений
services.Configure<ReceiverOptions>(s =>
{
    s.AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery];
});

// Инициализация настроек для чата
builder.AddOpenAIChatCompletion(modelId: modelId, apiKey: apiKey);

// Инициализация ядра
var kernel = builder.Build();

// Запуск телеграм бота
var telegramBotService = kernel.GetRequiredService<TelegramBotService>();
telegramBotService.Start(kernel);

Console.WriteLine("Телеграм бот начал свою работу. Нажмите <Enter> для выхода.");
Console.ReadLine();