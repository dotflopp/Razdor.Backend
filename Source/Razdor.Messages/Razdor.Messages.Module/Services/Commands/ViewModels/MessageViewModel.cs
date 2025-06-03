using System.Text.Json.Serialization;
using Razdor.Messages.Domain;
using Razdor.Shared.Module;

namespace Razdor.Messages.Module.Services.Commands.ViewModels;

public record MessageViewModel(
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong Id,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong UserId,
    [property:JsonConverter(typeof(JsonStringULongConverter))]
    ulong ChannelId,
    string? Text,
    DateTimeOffset CreatedAt,
    MessageReferenceViewModel? Reference,
    Embed? Embed,
    bool IsPinned,
    DateTimeOffset? EditedAt,
    IEnumerable<AttachmentViewModel>? Attachments,
    bool MentionedEveryone,
    IEnumerable<MentionedUserViewModel>? MentionedUsers,
    IEnumerable<MentionedChannelViewModel>? MentionedChannels,
    IEnumerable<MentionedRoleViewModel>? MentionedRoles
)
{
    public static MessageViewModel From(Message message)
    {
        MessageReferenceViewModel? reference = (message.Reference != null)
            ? MessageReferenceViewModel.From(message.Reference)
            : null;
        
        IEnumerable<AttachmentViewModel>? attachments = (message.Attachments.Count > 0)
            ? message.Attachments.Select(AttachmentViewModel.From)
            : null;
        IEnumerable<MentionedUserViewModel>? mentionedUsers = (message.MentionedUsers.Count > 0)
            ? message.MentionedUsers.Select(MentionedUserViewModel.From)
            : null;
        IEnumerable<MentionedChannelViewModel>? mentionedChannels = (message.MentionedChannels.Count > 0)
            ? message.MentionedChannels.Select(MentionedChannelViewModel.From)
            : null;
        IEnumerable<MentionedRoleViewModel>? mentionedRoles = (message.MentionedRoles.Count > 0)
            ? message.MentionedRoles.Select(MentionedRoleViewModel.From)
            : null;
        
        return new MessageViewModel(
            message.Id,
            message.UserId,
            message.ChannelId,
            message.Text,
            message.CreatedAt,
            reference,
            message.Embed,
            message.IsPinned,
            message.EditedAt,
            attachments,
            message.MentionedEveryone,
            mentionedUsers,
            mentionedChannels,
            mentionedRoles
        );
    }
};