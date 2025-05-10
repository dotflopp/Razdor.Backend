namespace Razdor.Communities.Services.Contracts;

public interface ICommunitiesModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommunitiesCommand command);
    Task ExecuteCommandAsync(ICommunitiesCommand command);
    Task<TResult> ExecuteQueryAsync<TResult>(ICommunitiesQuery<TResult> query);
}