namespace Razdor.Shared.Domain.Repository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Cохраняет изменения и вызывает доменные ивенты, 
    /// </summary>
    /// <returns>количество сохраненных записей</returns>
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}