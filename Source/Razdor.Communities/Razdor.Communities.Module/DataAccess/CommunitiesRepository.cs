﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Razdor.Communities.Domain;
using Razdor.Shared.Domain.Repository;
using Razdor.Shared.Module.DataAccess;

namespace Razdor.Communities.Services.DataAccess;

public class CommunitiesRepository(
    CommunityDataContext context,
    UnitOfWork<CommunityDataContext> unitOfWork
) : ICommunitiesRepository
{
    private readonly CommunityDataContext _context = context;
    public IUnitOfWork UnitOfWork => unitOfWork;
    public Community Add(Community community)
    {
        EntityEntry<Community> entry = _context.Add(community);
        return entry.Entity;
    }

    public Community Update(Community community)
    {
        EntityEntry<Community> entry = _context.Update(community);
        return entry.Entity;
    }

    public async Task<Community?> FindAsync(ulong communityId, CancellationToken cancellationToken)
    {
        return await _context.Communities.FirstOrDefaultAsync(x => x.Id == communityId, cancellationToken);
    }

    public async Task<bool> ContainsAsync(ulong communityId)
    {
        return await _context.Communities.AnyAsync(x => x.Id == communityId);
    }
}