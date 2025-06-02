using Mediator;

namespace Razdor.Messaging.Module.Contracts;

public interface IMessagingCommand : ICommand, IMessagingCommand<Unit>;
public interface IMessagingCommand<out TResult> : ICommand<TResult>;