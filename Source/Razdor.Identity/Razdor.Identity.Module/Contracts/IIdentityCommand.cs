using Mediator;

namespace Razdor.Identity.Module.Contracts;

public interface IIdentityCommand : ICommand, IIdentityCommand<Unit>
{}

public interface IIdentityCommand<T> : ICommand<T>
{}

