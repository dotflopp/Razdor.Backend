using System.Data;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public static class RuleValidationHelper
{
    public static async Task ThrowIfBrokenAsync(params IEnumerable<IBusinessRuleAsyncValidator> rules)
    {
        foreach (var rule in rules)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinesRuleValidationException(rule);            
            }
        }
    }
    
    public static void ThrowIfBroken(params IEnumerable<IBusinessRuleValidator> rules)
    {
        foreach (var rule in rules)
        {
            if (rule.IsBroken())
            {
                throw new BusinesRuleValidationException(rule);            
            }
        }
    }
}