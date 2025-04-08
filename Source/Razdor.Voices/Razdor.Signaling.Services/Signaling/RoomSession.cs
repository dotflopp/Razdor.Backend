using Razdor.Signaling.Internal;

namespace Razdor.Signaling.Services.Signaling;

public record RoomSession(
    string Server,
    string SessionId
) : IRoomSession;