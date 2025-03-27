namespace Razdor.Shared.DataAccess.Abstractions;

public interface IUnitOfWorkRepository<T> : IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
}