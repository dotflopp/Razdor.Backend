using System.Collections.Concurrent;
using Razdor.Signaling.Internal;

namespace Razdor.Signaling.Services.Signaling;

public class SignalingInternalService(string server) : ISignalingInternalService
{
    private readonly IDictionary<ulong, Room> _rooms = new ConcurrentDictionary<ulong, Room>();
    private readonly string _server = server;

    public ulong Id { get; }
    public IDictionary<string, IRoom> UsersRooms { get; } = new ConcurrentDictionary<string, IRoom>();
    public IDictionary<string, string> SessionConnections { get; } = new ConcurrentDictionary<string, string>();


    public Task<IRoom> CreateIfNotExistRoomAsync(ulong channelId)
    {
        if (_rooms.TryGetValue(channelId, out Room? room))
            return Task.FromResult<IRoom>(room);

        room = new Room(channelId, _server);
        _rooms[channelId] = room;
        return Task.FromResult<IRoom>(room);
    }

    public Task<IRoom?> FindRoomAsync(ulong channelId)
    {
        if (_rooms.TryGetValue(channelId, out Room? room))
            return Task.FromResult<IRoom?>(room);

        return Task.FromResult<IRoom?>(null);
    }


    public async Task<IRoom?> FindRoomBySessionAsync(string sessionId)
    {
        string? channelIdStr = sessionId.Split("-").FirstOrDefault();

        if (!ulong.TryParse(channelIdStr, out ulong channelId))
            return null;

        return await FindRoomAsync(channelId);
    }
}