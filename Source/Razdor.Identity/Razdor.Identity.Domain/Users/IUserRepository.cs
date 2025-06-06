using Razdor.Shared.Domain.Repository;

namespace Razdor.Identity.Domain.Users;

public interface IUserRepository : IUnitOfWorkRepository<UserAccount>
{
    UserAccount Add(UserAccount user);
    Task<UserAccount> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<UserAccount> FindByIdAsync(ulong id, CancellationToken cancellationToken = default);
}