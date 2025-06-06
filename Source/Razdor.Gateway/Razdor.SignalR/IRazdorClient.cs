namespace Razdor.SignalR;

public interface IRazdorClient
{
    Task MessageCreated();
    Task ChannelCreated();
    Task CommunityMemberAdded();
}