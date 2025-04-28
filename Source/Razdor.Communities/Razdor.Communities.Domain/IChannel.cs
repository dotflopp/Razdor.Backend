using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public interface IChannel: IEntity
{
    string Name { get; }
    string Summary { get; }
    
}   