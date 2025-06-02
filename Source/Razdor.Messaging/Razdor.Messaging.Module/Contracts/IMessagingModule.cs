namespace Razdor.Messaging.Module.Contracts;

public interface IMessagingModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(IMessagingCommand<TResult> command, CancellationToken cancellationToken = default);
    Task ExecuteCommandAsync(IMessagingCommand command, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteQueryAsync<TResult>(IMessagingQuery<TResult> query, CancellationToken cancellationToken = default);
}