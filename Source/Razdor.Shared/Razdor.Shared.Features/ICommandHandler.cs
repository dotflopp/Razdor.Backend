using Mediator;

namespace Razdor.Shared.Features;

public interface ICommandHandler<in TCommand, TResult> : 
    IRequestHandler<TCommand, TResult> 
    where TCommand : ICommand<TResult>;