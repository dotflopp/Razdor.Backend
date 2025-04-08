namespace Razdor.Signaling.Internal;

public interface IRoomSession
{
    string SessionId { get; }
    string Server { get; }
}