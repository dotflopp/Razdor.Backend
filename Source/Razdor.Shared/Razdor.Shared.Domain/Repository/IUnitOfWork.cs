namespace Razdor.Shared.Domain.Repository;

public interface IUnitOfWork
{
    /// <summary>
    ///     Cохраняет изменения и вызывает доменные ивенты,
    /// </summary>
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}