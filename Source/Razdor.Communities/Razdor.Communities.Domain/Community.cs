using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Events;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;
using Razdor.Shared.Extensions;

namespace Razdor.Communities.Domain;

public class Community : BaseSnowflakeEntity, INamed, IEntity<ulong>
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 400;

    private List<Role>? _roles;

    /// <summary>
    ///     EF constructor
    /// </summary>
    private Community() : this(0, 0, null!, null, null, CommunityNotificationPolicy.Nothing, null!, null)
    {
    }

    internal Community(
        ulong id,
        ulong ownerId,
        string name,
        MediaFileMeta? avatar,
        string? description,
        CommunityNotificationPolicy defaultNotificationPolicy,
        EveryonePermissions everyone,
        List<Role>? roles
    ) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        Avatar = avatar;
        Description = description;
        DefaultNotificationPolicy = defaultNotificationPolicy;
        Everyone = everyone;
        _roles = roles;
    }
    
    public string Name { get; private set; }
    public ulong OwnerId { get; private set; }
    public MediaFileMeta? Avatar
    {
        get => field;
        set
        {
            AddDomainEvent(new CommunityAvatarChanged(Id, value));
            field = value;
        }
    }
    public string? Description { get; private set; }
    public CommunityNotificationPolicy DefaultNotificationPolicy { get; private set; }
    public EveryonePermissions Everyone { get; private set;  }

    /// <summary>
    ///     Должна быть отсортированная коллекция по ID роли
    /// </summary>
    public IReadOnlyCollection<Role> Roles => _roles?.AsReadOnly() ?? ReadOnlyCollection<Role>.Empty;


    public static Community CreateNew(ulong id, ulong ownerId, string name, MediaFileMeta? avatar, string? description)
    {
        if (id == 0)
            throw new ArgumentException("The community ID cannot be zero.", nameof(id));
        if (ownerId == 0)
            throw new ArgumentException("OwnerId cannot be zero.", nameof(id));

        ArgumentNullException.ThrowIfNull(name);

        List<Role>? initialRoles = null;

        return new Community(
            id,
            ownerId,
            name,
            avatar,
            description,
            CommunityNotificationPolicy.Nothing,
            EveryonePermissions.Default,
            initialRoles
        );
    }

    /// <summary>
    ///     Вычисляет приоритет для пользователя сообщества
    /// </summary>
    public UserPermissions GetPermissions(CommunityMember member)
    {
        if (member.UserId == OwnerId)
            return UserPermissions.All;

        return GetPermissions(member.RoleIds);
    }

    /// <summary>
    ///     Вычисляет наибольший приоритет для пользователя сообщества
    /// </summary>
    public uint GetHighestPriority(CommunityMember member)
    {
        if (member.UserId == OwnerId)
            return 0;

        return GetHighestPriority(member.RoleIds);
    }

    /// <summary>
    ///     Вычисляет права пользователя на основе ролей
    /// </summary>
    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    public UserPermissions GetPermissions(IEnumerable<ulong> roleIds)
    {
        UserPermissions permissions = Everyone.Permissions;
        if (Roles.Count <= 0)
            return permissions;

        foreach (Role role in GetIntersectionRoles(roleIds))
            permissions |= permissions;

        return permissions;
    }

    /// <summary>
    ///     Вычисляет приоритет пользователя на основе ролей
    /// </summary>
    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    public uint GetHighestPriority(IEnumerable<ulong> roleIds)
    {
        uint priority = Everyone.Priority;

        foreach (Role role in GetIntersectionRoles(roleIds))
        {
            if (priority > role.Priority)
                priority = role.Priority;
        }

        return priority;
    }

    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    public IEnumerable<Role> GetIntersectionRoles(IEnumerable<ulong> roleIds)
    {
        if (Roles.Count <= 0)
            yield break;

        using IEnumerator<Role> roles = Roles.GetEnumerator();
        if (!roles.MoveNext())
            yield break;
        
        foreach (ulong roleId in roleIds)
        {
            while (roleId > roles.Current.Id)
                if (!roles.MoveNext())
                    break;
            
            if (roleId == roles.Current.Id)
                yield return roles.Current;
            
            if (!roles.MoveNext())
                yield break;
        }
    }

    public Role? FindRoleById(ulong roleId)
    {
        if (_roles == null || _roles.Count < 0)
            return null;
        
        int index = _roles.BinarySearchBy(roleId, (role) => role.Id);
        if (index < 0)
            return null;

        return _roles[index];
    }

    public void AddRole(Role role)
    {
        _roles ??= new();
        
        //TODO роли определяют так же и приоритет, добавить события для работы с приоритетом у ролей.
        
        // По идее роль добавляется в конец, а так как у новой роли snowflake id > чем у старой,
        // то массив отсортирован в порядке возрастания
        _roles.Add(role);

        Everyone.Priority = _roles.Max(x => x.Priority) + 1;
    }
}