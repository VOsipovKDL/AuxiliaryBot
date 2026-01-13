using AuxiliaryBot.Services.Interfaces;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AuxiliaryBot.Services;

internal class ActionService : IAction
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public ActionService(IStorage memoryStorage, ITelegramBotClient telegramBotClient)
    {
        _memoryStorage = memoryStorage;
        _telegramClient = telegramBotClient;
    }

    public string? Process(Message message)
    {
        if (string.IsNullOrEmpty(message.Text)) return null;

        string actionType = _memoryStorage.GetSession(message.Chat.Id).ActionType;

        return actionType switch
        {
            "length" => GetMessageLength(message.Text),
            "sum" => GetSumNumbersFromMessageh(message.Text),
            _ => String.Empty
        };
    }

    public string GetMessageLength(string userMessage)
    {
        return $"Длина сообщения: {userMessage.Length} знаков";
    }

    public string GetSumNumbersFromMessageh(string userMessage)
    {
        var matches = Regex.Matches(userMessage, @"\d+");

        int sum = 0;
        foreach (Match match in matches)
        {
            if (int.TryParse(match.Value, out int number))
                sum += number;
        }

        return $"Сумма всех чисел в сообщении: {sum.ToString()}";
    }
}
