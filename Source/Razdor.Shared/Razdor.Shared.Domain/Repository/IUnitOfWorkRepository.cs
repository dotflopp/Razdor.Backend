namespace Razdor.Shared.Domain.Repository;

public interface IUnitOfWorkRepository<T> : IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}