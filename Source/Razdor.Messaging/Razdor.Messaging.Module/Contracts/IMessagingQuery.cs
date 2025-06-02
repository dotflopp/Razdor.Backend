using Mediator;

namespace Razdor.Messaging.Module.Contracts;

public interface IMessagingQuery<out TResult> : IQuery<TResult>;