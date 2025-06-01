using Mediator;

namespace Razdor.Communities.Module.Contracts;

public interface ICommunitiesCommand : ICommand, ICommunitiesCommand<Unit>
{
}

public interface ICommunitiesCommand<out TResult> : ICommand<TResult>;