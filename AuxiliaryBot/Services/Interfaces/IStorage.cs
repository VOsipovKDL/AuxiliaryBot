using AuxiliaryBot.Models;

namespace AuxiliaryBot.Services.Interfaces;

internal interface IStorage
{
    Session GetSession(long chatId);
}
