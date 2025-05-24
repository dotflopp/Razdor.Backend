namespace Razdor.Identity.Domain.Users;

public interface IUsersCounter
{
    Task<int> CountUserWithIdentityNameOrEmail(string identityName, string email);
}