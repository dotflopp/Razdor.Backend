using Razdor.Communities.Domain.Members;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Rules;

public class CantJoinSameCommunityTwice(
    ICommunityMembersRepository members,
    ulong communityId,
    ulong userId
): IBusinessRuleAsyncValidator {
    public string Message => "Can't join the same community twice.";
    public ErrorCode ErrorCode => ErrorCode.ReJoiningToCommunity;
    public Task<bool> IsBrokenAsync(CancellationToken cancellationToken = default)
    {
        return members.ContainsAsync(communityId, userId);
    }
}