using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;
using Razdor.Communities.Services;
using Razdor.Communities.Services.DataAccess;

namespace Razdor.Communities.Infrastructure.DataAccess;

public class CommunitiesMongoDataContext : DbContext, ICommunitiesDataContext
{
    public DbSet<CommunityChannel> Channels { get; private set; }
    public DbSet<VoiceChannel> Voices { get; private set; }
    public DbSet<CategoryChannel> Categories { get; private set; }
    public DbSet<MessageChannel> Messages { get; private set; }
    public DbSet<ForkChannel> Forks { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<CommunityUser> CommunityUsers { get; private set; }
    public DbSet<Community> Communities { get; private set; }
    
}