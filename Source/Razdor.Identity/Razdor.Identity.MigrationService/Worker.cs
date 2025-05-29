using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Razdor.Identity.Migrations;

public class Worker<TContext>(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService where TContext : DbContext
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using Activity? activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            TContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            // await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(TContext dbContext, CancellationToken cancellationToken)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    // private static async Task SeedDataAsync(TContext dbContext, CancellationToken cancellationToken)
    // {
    //     SupportTicket firstTicket = new()
    //     {
    //         Title = "Test Ticket",
    //         Description = "Default ticket, please ignore!",
    //         Completed = true
    //     };
    //
    //     var strategy = dbContext.Database.CreateExecutionStrategy();
    //     await strategy.ExecuteAsync(async () =>
    //     {
    //         // Seed the database
    //         await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    //         await dbContext.Tickets.AddAsync(firstTicket, cancellationToken);
    //         await dbContext.SaveChangesAsync(cancellationToken);
    //         await transaction.CommitAsync(cancellationToken);
    //     });
    // }
}