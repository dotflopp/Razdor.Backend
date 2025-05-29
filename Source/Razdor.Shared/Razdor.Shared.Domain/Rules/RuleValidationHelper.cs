namespace Razdor.Shared.Domain.Rules;

public static class RuleValidationHelper
{
    public static async Task ThrowIfBrokenAsync(params IEnumerable<IBusinessRuleAsyncValidator> rules)
    {
        foreach (IBusinessRuleAsyncValidator rule in rules)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinesRuleValidationException(rule);
            }
        }
    }

    public static void ThrowIfBroken(params IEnumerable<IBusinessRuleValidator> rules)
    {
        foreach (IBusinessRuleValidator rule in rules)
        {
            if (rule.IsBroken())
            {
                throw new BusinesRuleValidationException(rule);
            }
        }
    }
}