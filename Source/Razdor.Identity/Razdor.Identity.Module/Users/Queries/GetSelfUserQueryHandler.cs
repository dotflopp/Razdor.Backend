using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Users.Queries.Exceptions;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Users.Queries;

public class GetSelfUserQueryHandler(
    IdentityDbContext dbContext,
    IRequestSenderContext sender
) : IQueryHandler<GetSelfUserQuery, SelfUserViewModel>
{
    public async ValueTask<SelfUserViewModel> Handle(GetSelfUserQuery query, CancellationToken cancellationToken)
    {
        if (!sender.IsAuthenticated)
            ExceptionsHelper.ThrowUnauthenticatedException();

        UserAccount? user = await dbContext.UserAccounts
            .Where(x => x.Id == sender.User.Id)
            .FirstOrDefaultAsync();
        
        if (user is null)
            throw new UserNotFoundException($"The user with the ID {sender.User.Id} was not found");

        return new SelfUserViewModel(
            user.Id,
            user.Email,
            user.IdentityName,
            user.Nickname,
            user.Avatar,
            user.CredentialsChangeDate
        );
    }
}