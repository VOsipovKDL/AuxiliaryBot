using AuxiliaryBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AuxiliaryBot.Controllers;

internal class InlineKeyboardController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(IStorage memoryStorage, ITelegramBotClient telegramBotClient)
    {
        _memoryStorage = memoryStorage;
        _telegramClient = telegramBotClient;
    }
    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null)
            return;

        _memoryStorage.GetSession(callbackQuery.From.Id).ActionType = callbackQuery.Data;

        string actionTypeText = callbackQuery.Data switch
        {
            "length" => " Подсчет количества сивмолов",
            "sum" => " Сложение чисел",
            _ => String.Empty
        };

        await _telegramClient.SendMessage(callbackQuery.From.Id,
            $"<b>Выбран режим - {actionTypeText}.{Environment.NewLine}</b>", cancellationToken: ct, parseMode: ParseMode.Html);
    }
}
