namespace Razdor.Messages.Module.Contracts;

public interface IMessagesModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(IMessagesCommand<TResult> command, CancellationToken cancellationToken = default);
    Task ExecuteCommandAsync(IMessagesCommand command, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteQueryAsync<TResult>(IMessagesQuery<TResult> query, CancellationToken cancellationToken = default);
}