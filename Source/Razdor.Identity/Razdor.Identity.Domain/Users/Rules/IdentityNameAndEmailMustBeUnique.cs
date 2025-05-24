using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Identity.Domain.Users.Rules;

public class IdentityNameAndEmailMustBeUnique(IUsersCounter counter, string name, string email) : IBusinessRuleAsyncValidator
{
    public string Message { get; } = "Identity name and email must be unique";
    public ErrorCode ErrorCode { get; } = ErrorCode.IdentityNameAlreadyExists;
    
    public async Task<bool> IsBrokenAsync(CancellationToken cancellationToken)
    {
        int count = await counter.CountUserWithIdentityNameOrEmail(name, email);
        return count > 0;
    }
}