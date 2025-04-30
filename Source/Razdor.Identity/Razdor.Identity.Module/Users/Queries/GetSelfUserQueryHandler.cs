using Mediator;
using Razdor.Identity.DataAccess;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Module.Users.Queries;

public class GetMeQueryHandler(
    IdentityDbContext dbContext
): IQueryHandler<GetUserQuery, UserPreviewModel>
{
    public ValueTask<UserPreviewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //dbContext.UserAccounts.FindAsync()
        
        //return new UserPreviewViewModel(
                
        //)
    }
}