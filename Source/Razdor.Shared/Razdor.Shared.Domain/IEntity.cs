namespace Razdor.Shared.Domain;

public interface IEntity<out TId>
{
    TId Id { get; }

    bool IsTransient { get; }
}