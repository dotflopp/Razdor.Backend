namespace Razdor.Identity.Domain;

public interface IUsersCounter
{
    Task<int> CountUserWithEmailAsync(string email);
    Task<int> CountUserWithIdentityName(string identityName);
}