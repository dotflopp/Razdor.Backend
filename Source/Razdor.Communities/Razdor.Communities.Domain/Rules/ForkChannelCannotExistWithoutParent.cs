using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Rules;

public class ForkChannelCannotExistWithoutParent: IBusinessRule
{
    public string Message { get; } = "FordChanel cannot exist without a parent";
    public ErrorCode ErrorCode => ErrorCode.InvalidOperationException;
}