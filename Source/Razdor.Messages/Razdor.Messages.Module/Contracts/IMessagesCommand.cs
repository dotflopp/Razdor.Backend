using Mediator;

namespace Razdor.Messages.Module.Contracts;

public interface IMessagesCommand : ICommand, IMessagesCommand<Unit>;
public interface IMessagesCommand<out TResult> : ICommand<TResult>;