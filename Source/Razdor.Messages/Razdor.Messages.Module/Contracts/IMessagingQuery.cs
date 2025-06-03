using Mediator;

namespace Razdor.Messages.Module.Contracts;

public interface IMessagingQuery<out TResult> : IQuery<TResult>;