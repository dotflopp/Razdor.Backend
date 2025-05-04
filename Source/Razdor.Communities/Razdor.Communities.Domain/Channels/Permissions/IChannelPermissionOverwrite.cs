using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Domain.Channels;

public interface IChannelPermissionOverwrite
{
    ulong RawValue { get; }
    
    UserPermissions Apply(UserPermissions permissions);
}

internal static class ChannelPermissionOverwriteExtensions
{
    internal static void SetPermission(this IChannelPermissionOverwrite permissionOverwrite, PermissionsState newPermissions, int rank, ref ulong result)
    {
        int absoluteRank = (rank << 2);
        ulong value = permissionOverwrite.RawValue & ~(0b11u << absoluteRank);
        value |= (ulong)newPermissions << absoluteRank;
        result = value;
    }

    internal static PermissionsState GetPermission(this IChannelPermissionOverwrite permissionOverwrite, int rank)
    {
        uint value = (uint)(permissionOverwrite.RawValue >> (rank << 2)) & 0x11;

        if (value == 0b11)
            value = 0b00;

        return (PermissionsState)value;   
    }
}