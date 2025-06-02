using System.Text.Json.Serialization;
using Razdor.Messaging.Domain;
using Razdor.Messaging.Domain.Mentioning;
using Razdor.Shared.Module;

namespace Razdor.Messaging.Module.Services.Commands.ViewModels;

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
            message.Attachments.Select(AttachmentViewModel.From),
            message.MentionedUsers.Select(MentionedUserViewModel.From),
            message.MentionedChannels.Select(MentionedChannelViewModel.From),
            message.MentionedRoles.Select(MentionedRoleViewModel.From)
        );
    }
};