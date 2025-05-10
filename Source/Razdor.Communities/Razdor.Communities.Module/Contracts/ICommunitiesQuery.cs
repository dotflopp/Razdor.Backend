using Mediator;

namespace Razdor.Communities.Services.Contracts;

public interface ICommunitiesQuery<out TResult> : IQuery<TResult>;