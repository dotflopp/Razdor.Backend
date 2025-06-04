using Mediator;
using Razdor.Communities.Domain;
using Razdor.Communities.Domain.Members;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Exceptions;

namespace Razdor.Communities.Module.Services.Communities.InternalQueries;

public class GetCommunityMemberPermissionsQueryHandler(
    ICommunityMembersRepository members,
    ICommunitiesRepository communities
): IQueryHandler<GetCommunityMemberPermissions, (UserPermissions permissions, uint Priority)>
{
    public async ValueTask<(UserPermissions permissions, uint Priority)> Handle(
        GetCommunityMemberPermissions query, CancellationToken cancellationToken
    ){
        CommunityMember? member = await members.FindAsync(query.CommunityId, query.UserId, cancellationToken);

        if (member == null)
            CommunityMemberNotFoundException.Throw(query.CommunityId, query.UserId);

        Community? community = await communities.FindAsync(query.CommunityId, cancellationToken);

        if (community == null)
            throw new InvalidOperationException($"There is a CommunityMember({query.UserId}) for whom there is no Community({query.CommunityId})");

        UserPermissions permissions = community.GetPermissions(member);
        uint priority = community.GetHighestPriority(member);
        return (permissions,priority);
    }
}