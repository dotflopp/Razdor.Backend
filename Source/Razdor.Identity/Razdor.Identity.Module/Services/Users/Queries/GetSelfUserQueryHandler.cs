using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Services.Users.Queries.Exceptions;
using Razdor.Identity.Module.Services.Users.ViewModels;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Services.Users.Queries;

public sealed class GetSelfUserQueryHandler(
    IdentityDbContext dbSqlContext,
    IRequestSenderContextAccessor senderContext
) : IQueryHandler<GetSelfUserQuery, SelfUserViewModel>
{
    public async ValueTask<SelfUserViewModel> Handle(GetSelfUserQuery query, CancellationToken cancellationToken)
    {
        UserAccount? user = await dbSqlContext.UserAccounts
            .Where(x => x.Id == senderContext.User.Id)
            .FirstOrDefaultAsync(cancellationToken:cancellationToken);

        if (user is null)
            throw new UserNotFoundException($"The user with the ID {senderContext.User.Id} was not found");

        return new SelfUserViewModel(
            user.Id,
            user.Email,
            user.IdentityName,
            user.Nickname,
            user.Avatar?.SourceUrl,
            user.CredentialsChangeDate,
            user.SelectedStatus,
            user.DisplayedStatus,
            user.Description
        );
    }
}