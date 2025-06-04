using Razdor.Communities.Domain.Members;
using Razdor.Communities.Module.Services.Members.ViewModels;

namespace Razdor.Communities.Module.Services.Members;

public interface IUserProfileFiller
{
    Task<MemberProfileViewModel> FillAsync(ulong userId, MemberProfile profile, CancellationToken cancellationToken = default);
}