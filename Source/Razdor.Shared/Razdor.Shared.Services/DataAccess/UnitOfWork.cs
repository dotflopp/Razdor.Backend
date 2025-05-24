using Mediator;
using Microsoft.EntityFrameworkCore;
using Razdor.Shared.Domain.Repository;

namespace Razdor.Shared.Module.DataAccess;

public class UnitOfWork<T>(T context, IMediator mediator) : IUnitOfWork
    where T : DbContext
{
    /// <inheritdoc />
    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var writtenCount = await context.SaveChangesAsync(cancellationToken);
        await mediator.DispatchDomainEventsAsync(context);

        return writtenCount;
    }
}