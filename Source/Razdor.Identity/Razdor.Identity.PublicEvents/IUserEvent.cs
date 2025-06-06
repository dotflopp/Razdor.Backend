namespace Razdor.Identity.PublicEvents;

public interface IUserEvent
{
    public ulong UserId { get; }
}