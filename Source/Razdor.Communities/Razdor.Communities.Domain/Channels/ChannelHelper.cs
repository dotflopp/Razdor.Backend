using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public static class ChannelHelper
{
    private static void SetEntityPermissions(
        List<Overwrite> overwrites,
        ulong entityId,
        OverwritePermissions permissions,
        PermissionTarget target
    )
    {
        RemoveEntityPermissions(overwrites, entityId);
        
        Overwrite overwrite = new(
            entityId,
            target,
            permissions
        );
        
        overwrites.Add(overwrite);
    }

    public static void SetRolePermissions(List<Overwrite> overwrites, ulong roleId, OverwritePermissions overwrite)
        => SetEntityPermissions(overwrites, roleId, overwrite, PermissionTarget.Role);

    public static void SetUserPermissions(List<Overwrite> overwrites, ulong userId, OverwritePermissions overwrite)
        => SetEntityPermissions(overwrites, userId, overwrite, PermissionTarget.User);

    public static void RemoveEntityPermissions(
        List<Overwrite> overwrites, 
        ulong entityId
    ){
        int overwrite = overwrites.FindIndex(x => x.TargetId == entityId);
        
        if (overwrite < 0)
            return;
        
        overwrites.RemoveAt(overwrite);    
    }
    
    public static UserPermissions CalculateUserPermissions(this IOverwritesPermission channel, ICommunityUser user)
    {
        UserPermissions result = user.CommunityPermissions;
        HashSet<ulong> targetIds = new(user.Roles.Count() + 1);
        
        targetIds.Add(user.Id);
        foreach (IRole userRole in user.Roles)
            targetIds.Add(userRole.Id);
        
        foreach (Overwrite overwrite in channel.Overwrites)
        {
            if (!targetIds.Contains(overwrite.TargetId))
                continue;

            OverwritePermissions permissions = overwrite.Permissions;
            result = permissions.ApplyDeny(result);
        }
    
        foreach (Overwrite overwrite in channel.Overwrites)
        {
            if (!targetIds.Contains(overwrite.TargetId))
                continue;

            OverwritePermissions permissions = overwrite.Permissions;
            result = permissions.ApplyAllow(result);
        }

        return result;
    }
}