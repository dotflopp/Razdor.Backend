using System.Collections.Concurrent;
using Razdor.Signaling.Internal;

namespace Razdor.Signaling.Services.Signaling;

public class Room : IRoom
{
    private string _server;

    public Room(ulong channelId, string server)
    {
        Sessions = new ConcurrentDictionary<string, UserIdentity?>();
        ChannelId = channelId;
        _server = server;
    }
    
    public ulong ChannelId { get; }
    private IDictionary<string, UserIdentity?> Sessions { get; }
    
    public Task<IRoomSession> CreateUserSessionIfNotExistsAsync()
    {
        string newSessionId = $"{ChannelId}-{Guid.NewGuid()}";
        
        return Task.FromResult<IRoomSession>(new RoomSession(
            _server,
            newSessionId
        ));
    }

    public Task<IEnumerable<UserIdentity>> GetUsersAsync()
    {
        return Task.FromResult(
            Sessions.Values.Where(x => x != null).Cast<UserIdentity>()
        );
    }

    public Task<IEnumerable<string>> GetSessionsAsync()
    {
        return Task.FromResult<IEnumerable<string>>(
            Sessions.Keys
        );
    }

    public Task<UserIdentity?> FindUserAsync(string sessionId)
    {
        if (!Sessions.TryGetValue(sessionId, out UserIdentity? userIdentity))
            return Task.FromResult<UserIdentity?>(null);
        
        return Task.FromResult(userIdentity);
    }

    public async Task ConnectUserAsync(string sessionId, UserIdentity user)
    {
        Sessions[sessionId] = user;   
    }

    public Task DisconnectUserAsync(string sessionId)
    {
        Sessions.Remove(sessionId);
        return Task.CompletedTask;
    }
}