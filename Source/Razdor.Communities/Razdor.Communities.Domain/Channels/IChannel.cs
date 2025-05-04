using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Channels;

public interface IChannel : ISnowflakeEntity, IEntity<ulong>
{
    string Name { get; }
    
    void Rename(string newName);
}