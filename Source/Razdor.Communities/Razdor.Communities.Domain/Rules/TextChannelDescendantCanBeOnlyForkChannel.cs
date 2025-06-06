using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Rules;

public class TextChannelDescendantCanBeOnlyForkChannel(CommunityChannel child): IBusinessRuleValidator
{
    public string Message { get; }
    public ErrorCode ErrorCode { get; }
    
    public bool IsBroken(CancellationToken cancellationToken = default)
    {
        return child.Type != ChannelType.ForkChannel;
    }
}