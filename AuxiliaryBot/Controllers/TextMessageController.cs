using AuxiliaryBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AuxiliaryBot.Controllers;

internal class TextMessageController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;
    private readonly IAction _actionService;

    public TextMessageController(IStorage memoryStorage, ITelegramBotClient telegramBotClient, IAction actionService    )
    {
        _memoryStorage = memoryStorage;
        _telegramClient = telegramBotClient;
        _actionService=actionService;
    }
    public async Task Handle(Message message, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":

                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData($" Подсчет количества сивмолов" , $"length"),
                    InlineKeyboardButton.WithCallbackData($" Сложение чисел" , $"sum"),
                });

                await _telegramClient.SendMessage(message.Chat.Id, $"<b> Я бот-помошник.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Чем я могу вам помочь?{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                break;
            default:
                var resultText = _actionService.Process(message);
                if (string.IsNullOrEmpty(resultText)) break;

                await _telegramClient.SendMessage(message.Chat.Id, resultText, cancellationToken: ct);
                break;
        }
        
    }
}
