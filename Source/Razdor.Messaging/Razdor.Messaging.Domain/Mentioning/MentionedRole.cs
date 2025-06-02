namespace Razdor.Messaging.Domain.Mentioning;

public record MentionedRole(
    ulong CommunityId,
    ulong RoleId
);