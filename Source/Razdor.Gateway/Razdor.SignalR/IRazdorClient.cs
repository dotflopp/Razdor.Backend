using Razdor.Communities.PublicEvents.Events;
using Razdor.Communities.PublicEvents.ViewModels.Channels;
using Razdor.Communities.PublicEvents.ViewModels.Members;
using Razdor.Identity.PublicEvents.Event;
using Razdor.Messages.PublicEvents.ViewModels;

namespace Razdor.SignalR;

public interface IRazdorClient
{
    /// <summary>
    /// Было создано сообщение
    /// </summary>
    Task MessageCreated(MessageViewModel message);
    /// <summary>
    /// Был создан канала сообщества
    /// </summary>
    Task ChannelCreated(ChannelViewModel channel);
    /// <summary>
    ///  Был добавлен новый член сообщества
    /// </summary>
    Task CommunityMemberAdded(CommunityMemberPreviewModel member);
    /// <summary>
    /// Были изменены данные члена сообщества
    /// </summary>
    Task MemberChanged(MemberChangedPublicEvent notification);
    /// <summary>
        /// Были изменены данные пользователя
    /// </summary>
    Task UserChanged(UserChangedPublicEvent notification);
}