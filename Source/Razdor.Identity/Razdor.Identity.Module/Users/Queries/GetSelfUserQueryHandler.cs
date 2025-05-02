using Mediator;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Module.Users.ViewModels;
using Razdor.Shared.Module.RequestSenderContext;

namespace Razdor.Identity.Module.Users.Queries;

public class GetSelfUserQueryHandler(
    IdentityDbContext dbContext,
    IRequestSenderContext sender
) : IQueryHandler<GetUserQuery, UserPreviewModel>
{
    public ValueTask<UserPreviewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //dbContext.UserAccounts.FindAsync()

        //return new UserPreviewViewModel(

        //)
    }
}