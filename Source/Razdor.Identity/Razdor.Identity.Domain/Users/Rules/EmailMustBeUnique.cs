using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Identity.Domain.Users.Rules;

public class EmailMustBeUnique(IUsersCounter counter, string email): IBusinessRule
{
    public string Message { get; } = "This email already exists";
    public ErrorCode ErrorCode { get; } = ErrorCode.EmailAlreadyExists;
    
    public async Task<bool> IsBrokenAsync(CancellationToken cancellationToken = default)
    {
        int count = await counter.CountUserWithEmailAsync(email);
        return count > 0;
    }
}