﻿namespace Razdor.Communities.Module.Contracts;

public interface ICommunitiesModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommunitiesCommand<TResult> command, CancellationToken cancellationToken = default);
    Task ExecuteCommandAsync(ICommunitiesCommand command, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteQueryAsync<TResult>(ICommunitiesQuery<TResult> query, CancellationToken cancellationToken = default);
}