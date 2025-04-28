using Mediator;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Module.Users.Queries;

public class GetMeQueryHandler(
    IdentityDbContext dbContext
): IQueryHandler<GetUserQuery, UserPreviewViewModel>
{
    public ValueTask<UserPreviewViewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        dbContext.UserAccounts.FindAsync()
        
        return new UserPreviewViewModel(
                
        )
    }
}