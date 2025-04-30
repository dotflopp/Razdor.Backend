using Mediator;

namespace Razdor.Identity.Module.Contracts;

public interface IIdentityCommand : ICommand, IIdentityCommand<Unit>
{}

public interface IIdentityCommand<out TResult> : ICommand<TResult>
{}

