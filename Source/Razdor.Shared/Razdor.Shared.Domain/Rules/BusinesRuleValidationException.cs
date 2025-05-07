using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public class BusinesRuleValidationException(
    IBusinessRule rule
) : RazdorException(
    rule.ErrorCode,
    rule.Message
)
{
    public IBusinessRule Rule => rule;
};