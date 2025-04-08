namespace Razdor.Signaling.Internal;

public interface IRoom
{
    ulong ChannelId { get; }
    Task<IRoomSession> CreateUserSessionIfNotExistsAsync();
    Task<IEnumerable<UserIdentity>> GetUsersAsync();
    Task<IEnumerable<string>> GetSessionsAsync();
    Task<UserIdentity?> FindUserAsync(string sessionId);
    Task ConnectUserAsync(string sessionId, UserIdentity user);
    Task DisconnectUserAsync(string sessionId);
}