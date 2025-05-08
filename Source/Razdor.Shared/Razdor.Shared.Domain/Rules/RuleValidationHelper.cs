using System.Data;
using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public static class RuleValidationHelper
{
    public static async Task ThrowIfBrokenAsync(params IEnumerable<IBusinessRule> rules)
    {
        foreach (var rule in rules)
        {
            if (await rule.IsBrokenAsync())
            {
                throw new BusinesRuleValidationException(rule);            
            }
        }
    }

    public static Task ThrowIfBrokenAsync<TId>(this IEntity<TId> entity, params IEnumerable<IBusinessRule> rules)
        => ThrowIfBrokenAsync(rules);
}