
using Mediator;

namespace Razdor.Shared.Features;

public interface IQuery<out TResult> : IRequest<TResult>;