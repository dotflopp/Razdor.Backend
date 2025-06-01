using Mediator;

namespace Razdor.Communities.Module.Contracts;

public interface ICommunitiesQuery<out TResult> : IQuery<TResult>;