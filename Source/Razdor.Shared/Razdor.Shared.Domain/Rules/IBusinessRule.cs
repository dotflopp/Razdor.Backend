using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public interface IBusinessRule
{
    Task<bool> IsBrokenAsync(CancellationToken cancellationToken = default);
    string Message { get; }
    ErrorCode ErrorCode { get; }
}