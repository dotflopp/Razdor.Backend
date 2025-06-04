using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

/// <summary>
///     Переопределенный профиль пользователя для конкретного сообщества, по умолчанию будут браться данные из
///     Razdor.Identity
/// </summary>
public record MemberProfile(
    string? Nickname,
    MediaFileMeta? Avatar
);