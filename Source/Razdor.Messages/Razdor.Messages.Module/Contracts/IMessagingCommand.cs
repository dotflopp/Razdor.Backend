using Mediator;

namespace Razdor.Messages.Module.Contracts;

public interface IMessagingCommand : ICommand, IMessagingCommand<Unit>;
public interface IMessagingCommand<out TResult> : ICommand<TResult>;