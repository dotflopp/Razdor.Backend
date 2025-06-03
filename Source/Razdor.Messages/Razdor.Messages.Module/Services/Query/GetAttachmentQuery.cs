using Mediator;
using Razdor.Communities.Domain.Permissions;
using Razdor.Communities.Module.Authorization;
using Razdor.Messages.Domain;
using Razdor.Messages.Module.Contracts;
using Razdor.Messages.Module.Services.Commands.ViewModels;

namespace Razdor.Messages.Module.Services.Query;

public record GetAttachmentQuery(
    ulong ChannelId,
    ulong MessageId,
    ulong AttachmentId    
): IMessagesQuery<AttachmentFileViewModel>, IRequiredChannelPermissions
{
    public UserPermissions RequiredPermissions => UserPermissions.ViewChannel;
}