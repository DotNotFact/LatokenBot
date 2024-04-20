using Telegram.Bot.Types.Enums;
using LatokenBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace LatokenBot.Services.Actions;

internal class StartService(CancellationTokenService _cancellationTokenService, ITelegramBotClient _telegramBotClient)
{
    [Command("/start")]
    public async Task StartGame(Update update)
    {
        if (update?.Message?.From?.FirstName is not { } firstName)
            return;

        if (update.Message.From?.Id is not { } chatId)
            return;

        string text = $"👋 Привет, <code>{firstName}</code>!\n" +
            "Поздравляю с началом вашего пути в компанию Latoken, ведущую криптобиржу, которая входит в топ-20 мировых и признана одним из лучших удаленных работодателей по версии Forbes за 2022 год.\n" +
            "Для начала, предлагаю вам зарегистрироваться на наш предстоящий хакатон AI Web3. Это отличная возможность показать свои умения и возможно, стать частью нашей команды. Регистрация здесь: https://t.me/gpt_web3_hackathon/5280.\n" +
            "Не забудьте также ознакомиться с информацией о компании и подробностями хакатона:\n" +
            "- О компании: https://deliver.latoken.com/about\n" +
            "- О хакатоне: https://deliver.latoken.com/hackathon\n\n" +
            "- Задание для хакатона: https://docs.google.com/document/d/1PpkDg6BxwxSh-8qFqyiczbODrFH3qLa2VU57x5c1Oq0/\n" +
            "Если есть вопросы, смело задавайте. Удачи!\n\n";

        await _telegramBotClient.SendTextMessageAsync(
            chatId: chatId,
            text: text,
            parseMode: ParseMode.Html,
            cancellationToken: _cancellationTokenService.Token
            );
    }

    [Command("⬅️ назад")]
    public async Task OpenMenu(Update update)
    {
        if (update?.Message?.Chat?.Id is not { } chatId)
            return;

        await _telegramBotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Меню",
            cancellationToken: _cancellationTokenService.Token
            );
    }
}