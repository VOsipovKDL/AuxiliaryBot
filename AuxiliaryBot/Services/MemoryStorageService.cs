namespace AuxiliaryBot.Services;

using AuxiliaryBot.Models;
using AuxiliaryBot.Services.Interfaces;
using System.Collections.Concurrent;

internal class MemoryStorageService : IStorage
{
    private readonly ConcurrentDictionary<long, Session> _sessions;

    public MemoryStorageService()
    {
        _sessions = new ConcurrentDictionary<long, Session>();
    }

    public Session GetSession(long chatId)
    {
        if (_sessions.ContainsKey(chatId))
            return _sessions[chatId];

        var newSession = new Session() { ActionType = "length" };
        _sessions.TryAdd(chatId, newSession);
        return newSession;
    }
}
