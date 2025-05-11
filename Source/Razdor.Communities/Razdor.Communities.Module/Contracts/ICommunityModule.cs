namespace Razdor.Communities.Services.Contracts;

public interface ICommunityModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommunitiesCommand<TResult> command, CancellationToken cancellationToken = default);
    Task ExecuteCommandAsync(ICommunitiesCommand command, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteQueryAsync<TResult>(ICommunitiesQuery<TResult> query, CancellationToken cancellationToken = default);
}