using Razdor.Shared.Domain.Exceptions;
using Razdor.Shared.Domain.Rules;

namespace Razdor.Communities.Domain.Rules;

public class ForkAndVoiceChannelsСannotHaveDescendants : IBusinessRule
{
    public string Message { get; } = "Fork and Voice channels cannot have descendants";
    public ErrorCode ErrorCode { get; } = ErrorCode.InvalidOperationException;
}