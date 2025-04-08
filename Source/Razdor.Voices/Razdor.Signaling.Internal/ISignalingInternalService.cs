namespace Razdor.Signaling.Internal;

public interface ISignalingInternalService
{
    ulong Id { get; }
    IDictionary<string, IRoom> UsersRooms { get; }
    IDictionary<string, string> SessionConnections { get; }
    Task<IRoom> CreateIfNotExistRoomAsync(ulong channelId);
    Task<IRoom?> FindRoomAsync(ulong channelId);
    Task<IRoom?> FindRoomBySessionAsync(string sessionId);
}