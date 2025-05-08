namespace Razdor.Identity.Domain.Users;

public interface IUsersCounter
{
    Task<int> CountUserWithEmailAsync(string email);
    Task<int> CountUserWithIdentityName(string identityName);
}