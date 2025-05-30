using Razdor.Communities.Domain.Channels;

namespace Razdor.Communities.Domain.Permissions;

public record Overwrite
{
    /// <summary>
    /// EF Constructor
    /// </summary>
    private Overwrite():this(0, (PermissionTargetType)0, null!)
    {
    }

    public Overwrite(
        ulong TargetId,
        PermissionTargetType TargetType,
        OverwritePermissions Permissions
    ){
        this.TargetId = TargetId;
        this.TargetType = TargetType;
        this.Permissions = Permissions;
    }
    
    public ulong TargetId { get; private set; }
    public PermissionTargetType TargetType { get; private set; }
    public OverwritePermissions Permissions { get; private set; }
}