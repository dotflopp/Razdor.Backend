using Razdor.Communities.Domain.Channels;
using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Rules;

public class CategoryChannelCannotHaveForkChannel(CommunityChannel channel): IBusinessRuleValidator
{
    public string Message { get; } = "Category channel cannot have fork channel";
    public ErrorCode ErrorCode { get; } = ErrorCode.InvalidOperationException;
    
    public bool IsBroken(CancellationToken cancellationToken = default)
    {
        return channel.Type == ChannelType.ForkChannel;
    }
}