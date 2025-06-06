using Mediator;
using Razdor.Communities.Domain.Permissions;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.Contracts;
using Razdor.Shared.Module.Authorization;
using Razdor.Shared.Module.Media;

namespace Razdor.Messages.Module.Services.Query;

public record GetAttachmentQuery(
    ulong ChannelId,
    ulong MessageId,
    ulong AttachmentId    
): IMessagesQuery<MediaFile>, IRequiredChannelPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ViewChannel;
}