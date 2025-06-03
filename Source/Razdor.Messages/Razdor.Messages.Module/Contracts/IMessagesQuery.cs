using Mediator;

namespace Razdor.Messages.Module.Contracts;

public interface IMessagesQuery<out TResult> : IQuery<TResult>;