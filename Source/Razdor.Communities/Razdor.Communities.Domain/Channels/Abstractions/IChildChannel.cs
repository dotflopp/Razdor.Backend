namespace Razdor.Communities.Domain.Channels.Abstractions;

public interface IChildChannel: ICommunityChannel
{
    /// <summary>
    /// CategoryChannel либо родительского MessageChannel для ForkChannel
    /// </summary>
    ICommunityChannel? Parent { get; }
    
    /// <summary>
    /// Указывает на то что права синхронизируются с родительским каналом
    /// </summary>
    bool IsSyncing { get; }
}