using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public interface ICommunityUser: IUser,  ICommunityEntity<ulong>, ISnowflakeEntity, IEntity<ulong>
{
    /// <summary>
    /// Указывает на то что пользователь является владельцем сообщества
    /// </summary>
    bool IsOwner { get; }
    
    /// <summary>
    /// Переопределенная политика уведомлений для конкретного пользователя
    /// </summary>
    CommunityNotificationPolicy? NotificationPolicy { get; }
    
    /// <summary>
    /// Дата последнего присоединения к сообществу
    /// </summary>
    DateTimeOffset JoiningDate { get; }
    
    VoiceState VoiceState { get; }

    
    /// <summary>
    /// Переопределенный для сообщества профиль, если есть
    /// </summary>
    UserCommunityProfile? CommunityProfile { get; }
    
    /// <summary>
    /// Роли пользователя в сообществе
    /// </summary>
    IReadOnlyCollection<Role> Roles { get; }

    /// <summary>
    /// Права пользователя в сообществе на основе всех ролей, у Owner всегда право UserPermissions.Administrator
    /// </summary>
    public UserPermissions GetCommunityPermissions();

    /// <summary>
    /// Наивысший приоритет (наименьшее значение приоритета у Roles, 0 для владелеца сообщества) пользователя 
    /// </summary>
    public ulong GetHighestPriority();
}