﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Channels;
using Razdor.Communities.Domain.Invites;
using Razdor.Communities.Domain.Members;

namespace Razdor.Communities.Module.DataAccess;

[method:RequiresUnreferencedCode("EF Core isn't fully compatible with trimming, and running the application may generate unexpected runtime failures. Some specific coding pattern are usually required to make trimming work properly, see https://aka.ms/efcore-docs-trimming for more details.")]
[method:RequiresDynamicCode("EF Core isn't fully compatible with NativeAOT, and running the application may generate unexpected runtime failures.")]
public abstract class CommunitiesDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CommunityChannel> Channels { get; protected set; }
    public DbSet<VoiceChannel> Voices { get; protected set; }
    public DbSet<CategoryChannel> Categories { get; protected set; }
    public DbSet<TextChannel> Messages { get; protected set; }
    public DbSet<ForkChannel> Forks { get; protected set; }

    public DbSet<CommunityMember> CommunityMembers { get; protected set; }
    public DbSet<Community> Communities { get; protected set; }
    public DbSet<Invite> Invites { get; protected set; }
}