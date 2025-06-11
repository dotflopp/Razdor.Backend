using System.Text.Json.Serialization;
using Razdor.Identity.Domain.Events;
using Razdor.Identity.Domain.Users;
using Razdor.Shared.Domain;
using Razdor.Shared.IntegrationEvents;
using Razdor.Shared.Module.Serialization;

namespace Razdor.Identity.PublicEvents.Event;

public record UserChangedPublicEvent(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId,
    UserProperties Changes,
    string? Nickname = null,
    SelectedCommunicationStatus? SelectedStatus = null,
    DisplayedCommunicationStatus? Status = null,
    DateTimeOffset? CredentialsChangeDate = null,
    bool? IsOnline = null,
    string? Avatar = null,
    string? Description = null
) : IPublicEvent, IUserEvent
{
    public static UserChangedPublicEvent From(UserChangedEvent changedEvent)
    {
        return new UserChangedPublicEvent(
            changedEvent.UserId,
            changedEvent.UserProperties,
            changedEvent.Nickname,
            changedEvent.SelectedStatus,
            changedEvent.DisplayedStatus,
            changedEvent.CredentialsChangeDate,
            changedEvent.IsOnline,
            changedEvent.Avatar?.SourceUrl,
            changedEvent.Description
        );
    }
};