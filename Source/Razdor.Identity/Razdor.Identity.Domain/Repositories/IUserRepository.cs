using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.Domain.Repositories;

public interface IUserRepository : IUnitOfWorkRepository<UserAccount>
{
    UserAccount Add(UserAccount user);
    Task<UserAccount?> FindByEmailAsync(string email);
}