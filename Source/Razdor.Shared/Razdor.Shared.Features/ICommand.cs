using Mediator;

namespace Razdor.Shared.Features;

public interface ICommand<out TResult> : IRequest<TResult>;