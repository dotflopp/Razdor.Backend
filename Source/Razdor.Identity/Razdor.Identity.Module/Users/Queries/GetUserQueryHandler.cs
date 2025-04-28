using Mediator;
using Razdor.Identity.Module.Users.ViewModels;

namespace Razdor.Identity.Module.Users.Queries;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserPreviewViewModel>
{
    public ValueTask<UserPreviewViewModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}