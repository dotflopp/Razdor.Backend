using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Users.Queries.Exceptions;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module.Exceptions;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Users.Queries;

public class GetSelfUserQueryHandler(
    IIdentityDbContext dbSqlContext,
    IRequestSenderContext sender
) : IQueryHandler<GetSelfUserQuery, SelfUserViewModel>
{
    public async ValueTask<SelfUserViewModel> Handle(GetSelfUserQuery query, CancellationToken cancellationToken)
    {
        if (!sender.IsAuthenticated)
            ExceptionsHelper.ThrowUnauthenticatedException();

        UserAccount? user = await dbSqlContext.UserAccounts
            .Where(x => x.Id == sender.User.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (user is null)
            throw new UserNotFoundException($"The user with the ID {sender.User.Id} was not found");

        return new SelfUserViewModel(
            user.Id,
            user.Email,
            user.IdentityName,
            user.Nickname,
            user.Avatar,
            user.CredentialsChangeDate,
            user.SelectedStatus,
            user.DisplayedStatus,
            user.Description
        );
    }
}