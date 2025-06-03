namespace Razdor.Messages.Domain.Mentioning;

public record MentionedRole(
    ulong CommunityId,
    ulong RoleId
);