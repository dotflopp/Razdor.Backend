namespace Razdor.Communities.Domain.Permissions;

public readonly record struct OverwritePermissions(
    UserPermissions Allow,
    UserPermissions Deny
){
    public static readonly OverwritePermissions Default = new(
        UserPermissions.None,
        UserPermissions.None
    );

    public UserPermissions ApplyDeny(UserPermissions permissions)
    {
        return ~Deny & permissions;
    }

    public UserPermissions ApplyAllow(UserPermissions permissions)
    {
        return Allow | permissions;
    }
}