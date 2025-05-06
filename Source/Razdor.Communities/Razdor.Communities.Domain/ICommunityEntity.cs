using Razdor.Communities.Domain.Communities;
using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain;

public interface ICommunityEntity<out T> : IEntity<T>
{
    Community Community { get; }
}