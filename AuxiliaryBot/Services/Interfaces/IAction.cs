using AuxiliaryBot.Models;
using Telegram.Bot.Types;

namespace AuxiliaryBot.Services.Interfaces;

internal interface IAction
{
    public string? Process(Message message);
}
