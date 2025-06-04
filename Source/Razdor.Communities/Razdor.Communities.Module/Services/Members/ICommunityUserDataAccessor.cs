using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

public interface ICommunityUserDataAccessor
{
    Task<UserDataViewModel> FillAsync(ulong userId, MemberProfile profile, CancellationToken cancellationToken = default);
}