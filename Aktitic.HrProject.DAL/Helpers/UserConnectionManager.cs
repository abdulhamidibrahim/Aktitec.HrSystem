using System.Collections.Concurrent;

namespace Aktitic.HrProject.DAL.Helpers;



public class UserConnectionManager
{
    private static readonly ConcurrentDictionary<string, string> _userConnections = new();

    public void AddConnection(string userId, string connectionId)
    {
        _userConnections[connectionId] = userId;
    }

    public void RemoveConnection(string connectionId)
    {
        _userConnections.TryRemove(connectionId, out _);
    }

    public string GetUserId(string connectionId)
    {
        _userConnections.TryGetValue(connectionId, out var userId);
        return userId;
    }
}