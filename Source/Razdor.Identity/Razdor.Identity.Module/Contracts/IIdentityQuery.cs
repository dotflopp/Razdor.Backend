using Mediator;

namespace Razdor.Identity.Module.Contracts;

public interface IIdentityQuery<out TResult> : IQuery<TResult>
{
}