using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public interface ICommunityUser: IUser, ISnowflakeEntity, IEntity<ulong>
{
    /// <summary>
    /// Указывает на то что пользователь является владельцем сообщества
    /// </summary>
    bool IsOwner { get; }
    
    /// <summary>
    /// Переопределенная политика уведомлений для конкретного пользователя
    /// </summary>
    CommunityNotificationPolicy? NoficationPolicy { get; }
    
    /// <summary>
    /// Переопределенный для сообщества профиль, если есть
    /// </summary>
    UserCommunityProfile? CommunityProfile { get; }
    
    /// <summary>
    /// Роли пользователя в сообществе
    /// </summary>
    IReadOnlyCollection<IRole> Roles { get; }
    
    /// <summary>
    /// Права пользователя в сообществе на основе всех ролей
    /// </summary>
    UserPermissions CommunityPermissions { get; }
    
    /// <summary>
    /// Наивысший приоритет (наименьшее значение среди приоритета у Roles, 0 если владелец сообщества) пользователя 
    /// </summary>
    ulong HighestPriority { get; }
}