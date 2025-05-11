using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Channels.Abstractions;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Roles;

namespace Razdor.Communities.Services.DataAccess;

public interface ICommunitiesDataContext
{
    DbSet<CommunityChannel> Channels { get; }
    DbSet<VoiceChannel> Voices { get; }
    DbSet<CategoryChannel> Categories { get; }
    DbSet<MessageChannel> Messages { get; }
    DbSet<ForkChannel> Forks { get; }

    DbSet<Role> Roles { get; }
    DbSet<CommunityUser> CommunityUsers { get; }
    DbSet<Community> Communities { get; }
}