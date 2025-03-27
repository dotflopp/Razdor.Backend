namespace Razdor.Shared.DataAccess.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Вызывает доменные ивенты, сохраняет изменения
    /// </summary>
    /// <returns>количество сохраненных записей</returns>
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}