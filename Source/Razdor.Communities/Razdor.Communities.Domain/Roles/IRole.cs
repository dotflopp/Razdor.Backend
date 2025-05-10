using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Permissions;

/// <summary>
/// Без ролей у пользователя нету разрешений
/// Роль выдает разрешение пользователю, но не может его забрать
/// Но некоторые разрешение можно переопределить в канале
/// </summary>
public interface IRole: INamed, ISnowflakeEntity, IEntity<ulong>
{
    ulong CommunityId { get; }
    UserPermissions Permissions { get; }
    /// <summary>
    /// Указывает на то что роль можно упоминать
    /// </summary>
    bool IsMentioned { get; }
    /// <summary>
    /// Чем ниже число тем важнее роль, люди с более высоким приоритетом могут управлять людьми с более низким
    /// От 1, 0 зарезервирован для создателя сообщества
    /// </summary>
    uint Priority { get; }
}