﻿using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Identity.Domain.Users;
using Razdor.Identity.Module.DataAccess;
using Razdor.Identity.Module.Services.Users.Queries.Exceptions;
using Razdor.Identity.Module.Services.Users.ViewModels;

namespace Razdor.Identity.Module.Services.Users.Queries;

public sealed class GetUserQueryHandler(
    IdentityDbContext dbSqlContext
) : IQueryHandler<GetUserQuery, UserPreviewModel>
{
    public async ValueTask<UserPreviewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        UserAccount? user = await dbSqlContext.UserAccounts
            .Where(x => x.Id == query.UserId)
            .FirstOrDefaultAsync(cancellationToken:cancellationToken);

        if (user is null)
            throw new UserNotFoundException($"The user with the ID {query.UserId} was not found");

        return new UserPreviewModel(
            user.Id,
            user.IdentityName,
            user.Nickname,
            user.Avatar?.SourceUrl,
            user.DisplayedStatus,
            user.Description
        );
    }
}