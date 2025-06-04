using System.Text.Json.Serialization;
using Razdor.Communities.Domain.Invites;
using Razdor.Shared.Domain;

namespace Razdor.Shared.Module;

public record StringIdViewModel(
    string Id
)
{
    public static StringIdViewModel From<T>(T entity)
        where T : IEntity<string>
    {
        return new StringIdViewModel(entity.Id);
    }
}

public record SnowflakeIdViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id
)
{
    public static SnowflakeIdViewModel From<T>(T entity)
        where T : IEntity<ulong>
    {
        return new SnowflakeIdViewModel(entity.Id);
    }
};