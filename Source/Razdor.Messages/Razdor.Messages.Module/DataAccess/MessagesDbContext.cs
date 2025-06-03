using Microsoft.EntityFrameworkCore;
using Razdor.Messages.Domain;

namespace Razdor.Messages.Module.DataAccess;

public abstract class MessagesDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<Message> Messages { get; protected set; }
}