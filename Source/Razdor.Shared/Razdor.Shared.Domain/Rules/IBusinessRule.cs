using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Domain.Rules;

public interface IBusinessRule
{
    Task<bool> IsBrokenAsync();
    string Message { get; }
    ErrorCode ErrorCode { get; }
}