using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.Users.Queries.Exceptions;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Module.Users.Queries;

public class GetUserQueryHandler(
    IdentityDbContext dbContext
) : IQueryHandler<GetUserQuery, UserPreviewModel>
{
    public async ValueTask<UserPreviewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        UserAccount? user = await dbContext.UserAccounts
            .Where(x => x.Id == query.UserId)
            .FirstOrDefaultAsync();
        
        if (user is null)
            throw new UserNotFoundException($"The user with the ID {query.UserId} was not found");

        return new UserPreviewModel(
            user.Id,
            user.IdentityName,
            user.Nickname,
            user.Avatar
        );
    }
}