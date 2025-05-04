using Razdor.Shared.Domain;

namespace Razdor.Communities.Domain.Members;

public interface IUser: ISnowflakeEntity, IEntity<ulong>
{
}