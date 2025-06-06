using Razdor.Messages.Domain.Mentioning;
using Razdor.Shared.Domain;

namespace Razdor.Messages.Domain.Events;

public record MessageCreatedEvent(
    Message Message
) : IDomainEvent
{
    public static MessageCreatedEvent From(Message message)
        => new MessageCreatedEvent(message);
}