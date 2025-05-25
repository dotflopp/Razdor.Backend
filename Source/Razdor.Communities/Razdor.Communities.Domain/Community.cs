using System.Collections.ObjectModel;
using Razdor.Communities.Domain.Event;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Domain.Roles;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public class Community : BaseSnowflakeEntity, INamed, IEntity<ulong>
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 400;

    private readonly List<Role>? _roles;

    /// <summary>
    /// EF constructor
    /// </summary>
    private Community() : this(0, 0, null, null, null, CommunityNotificationPolicy.Nothing, null)
    {
    }

    public Community(
        ulong id,
        ulong ownerId,
        string name,
        string? avatar,
        string? description,
        CommunityNotificationPolicy defaultNotificationPolicy,
        EveryonePermissions everyone,
        List<Role>? roles = null
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

    public ulong OwnerId { get; private set; }
    public string Name { get; private set; }
    public string? Avatar { get; private set; }
    public string? Description { get; private set; }
    public CommunityNotificationPolicy DefaultNotificationPolicy { get; private set; }
    public EveryonePermissions Everyone { get; private set; }
    
    /// <summary>
    /// Должна быть отсортированая коллекция по ID роли
    /// </summary>
    public IReadOnlyCollection<Role> Roles => _roles?.AsReadOnly() ?? ReadOnlyCollection<Role>.Empty;

    public static Community CreateNew(ulong id, ulong ownerId, string name, string? avatar, string? description)
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
    /// Вычисляет приоритет для пользователя сообщества
    /// </summary>
    public UserPermissions GetPermissions(CommunityMember member)
    {
        if (member.Id == OwnerId)
            return UserPermissions.All;

        return GetPermissions(member.RoleIds);
    }
    
    /// <summary>
    /// Вычисляет наибольший приоритет для пользователя сообщества
    /// </summary>
    public ulong GetHighestPriority(CommunityMember member)
    {
        if (member.Id == OwnerId)
            return 0;

        return GetHighestPriority(member.RoleIds);
    }

    /// <summary>
    /// Вычисляет права пользователя на основе ролей
    /// </summary>
    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    private UserPermissions GetPermissions(IEnumerable<ulong> roleIds)
    {
        UserPermissions permissions = Everyone.Permissions;
        if (Roles.Count <= 0)
            return permissions;

        foreach (Role role in GetIntersectionRoles(roleIds))
            permissions |= permissions;
        
        return permissions;
    }
    
    /// <summary>
    /// Вычисляет приоритет пользователя на основе ролей
    /// </summary>
    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    private ulong GetHighestPriority(IEnumerable<ulong> roleIds)
    {
        ulong priority = Everyone.Priority;
        
        foreach (Role role in GetIntersectionRoles(roleIds))
        {
            if (priority > role.Priority)
                priority = role.Priority;
        }
        
        return priority;
    }
    
    /// <param name="roleIds">!Отсортированная по Id в порядке возрастания коллекция ролей</param>
    private IEnumerable<Role> GetIntersectionRoles(IEnumerable<ulong> roleIds)
    {
        if (Roles.Count <= 0)
            yield break;
        
        using IEnumerator<Role> roles = Roles.GetEnumerator();
        
        foreach (ulong roleId in roleIds)
        {
            while (roleId > roles.Current.Id)
                if (!roles.MoveNext()) 
                    yield break;
            
            if (roleId == roles.Current.Id)
                yield return roles.Current;
        }
    }
}