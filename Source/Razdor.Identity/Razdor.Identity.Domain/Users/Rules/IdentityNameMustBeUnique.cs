using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Identity.Domain.Users.Rules;

public class IdentityNameMustBeUnique(IUsersCounter counter, string name) : IBusinessRule
{
    public string Message { get; } = "This identity name already exists";
    public ErrorCode ErrorCode { get; } = ErrorCode.IdentityNameAlreadyExists;
    
    public async Task<bool> IsBrokenAsync(CancellationToken cancellationToken)
    {
        int count = await counter.CountUserWithIdentityName(name);
        return count > 0;
    }
}