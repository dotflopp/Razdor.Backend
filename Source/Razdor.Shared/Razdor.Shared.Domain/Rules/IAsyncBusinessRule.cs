using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public interface IBusinessRule
{
    string Message { get; }
    ErrorCode ErrorCode { get; }
}

public interface IBusinessRuleValidator : IBusinessRule
{
    bool IsBroken(CancellationToken cancellationToken = default);
}

public interface IBusinessRuleAsyncValidator : IBusinessRule
{
    Task<bool> IsBrokenAsync(CancellationToken cancellationToken = default);
}