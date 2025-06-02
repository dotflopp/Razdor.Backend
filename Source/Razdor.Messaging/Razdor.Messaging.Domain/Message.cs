using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Razdor.Messaging.Domain.Mentioning;
using Razdor.Shared.Domain;

namespace Razdor.Messaging.Domain;

public class Message: BaseSnowflakeEntity, IEntity<ulong>
{
    private Mentions _mentions;
    private List<Attachment>? _attachments;
    
    internal Message(
        ulong id, 
        ulong communityId,
        ulong userId,
        ulong channelId,
        string? text,
        DateTimeOffset createdAt,
        MessageReference? messageReference = null,
        Embed? embed = null,
        bool isMentionEveryone = false,
        bool isPinned = false,
        DateTimeOffset? updatedAt = null,
        List<Attachment>? attachments = null,
        Mentions? mentions = null
    ) : base(id)
    {
        UserId = userId;
        CommunityId = communityId;
        ChannelId = channelId;
        MessageReference = messageReference;
        Text = text;
        Embed = embed;
        IsPinned = isPinned;
        CreatedAt = createdAt;
        EditedAt = updatedAt;
        _mentions = mentions ?? new Mentions();
        _attachments = attachments;
    }

    public ulong UserId { get; private set; }
    public ulong ChannelId { get; private set; }
    public ulong CommunityId { get; private set; }
    public MessageReference? MessageReference { get; private set; }
    public string? Text { get; set; }
    public Embed? Embed { get; set; }
    public bool IsPinned { get; set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? EditedAt { get; private set; }
    
    public bool MentionEveryone => _mentions.EveryoneIsMentioned;
    public IReadOnlyCollection<MentionedUser> UserMentions => _mentions.Users;
    public IReadOnlyCollection<MentionedChannel> MentionedChannels => _mentions.Channels;
    public IReadOnlyCollection<MentionedRole> MentionedRoles =>_mentions.Roles;
    public IReadOnlyCollection<Attachment> Attachments => _attachments?.AsReadOnly() ?? ReadOnlyCollection<Attachment>.Empty;

    public static Message CreateNew(
        ulong id, 
        ulong communityId,
        ulong userId,
        ulong channelId,
        string? text,
        DateTimeOffset createdAt,
        MessageReference? messageReference = null,
        Embed? embed = null,
        List<Attachment>? attachments = null
    ){
        Mentions mentions = MentionsHelper.ExtractMentions(text, embed);
        return new Message(
            id, 
            communityId, 
            userId, 
            channelId, 
            text,
            createdAt, 
            messageReference,
            embed, 
            attachments: attachments,
            mentions: mentions
        );
    }
}