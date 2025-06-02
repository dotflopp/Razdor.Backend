using System.Collections.ObjectModel;
using Razdor.Messaging.Domain.Events;
using Razdor.Messaging.Domain.Mentioning;
using Razdor.Shared.Domain;

namespace Razdor.Messaging.Domain;

public class Message: BaseSnowflakeEntity, IEntity<ulong>
{
    private List<Attachment>? _attachments;

    /// <summary>
    /// EF constructor
    /// </summary>
    private Message(): this(0,0,0,null,default)
    {
    }

    internal Message(
        ulong id,
        ulong userId,
        ulong channelId,
        string? text,
        DateTimeOffset createdAt,
        MessageReference? reference = null,
        Embed? embed = null,
        bool isPinned = false,
        DateTimeOffset? editedAt = null,
        List<Attachment>? attachments = null,
        Mentions? mentions = null
    ) : base(id)
    {
        UserId = userId;
        ChannelId = channelId;
        Reference = reference;
        Text = text;
        Embed = embed;
        IsPinned = isPinned;
        CreatedAt = createdAt;
        EditedAt = editedAt;
        Mentions = mentions ?? new Mentions();
        _attachments = attachments;
    }

    public ulong UserId { get; private set; }
    public ulong ChannelId { get; private set; }
    public MessageReference? Reference { get; private set; }
    public string? Text { get; set; }
    public Embed? Embed { get; set; }
    public bool IsPinned { get; set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? EditedAt { get; private set; }
    
    public bool IsMentionEveryone => Mentions.EveryoneIsMentioned;
    public Mentions Mentions { get; private set; }
    public IReadOnlyCollection<MentionedUser> MentionedUsers => Mentions.Users;
    public IReadOnlyCollection<MentionedChannel> MentionedChannels => Mentions.Channels;
    public IReadOnlyCollection<MentionedRole> MentionedRoles =>Mentions.Roles;
    public IReadOnlyCollection<Attachment> Attachments => _attachments?.AsReadOnly() ?? ReadOnlyCollection<Attachment>.Empty;

    public static Message CreateNew(
        ulong id, 
        ulong userId,
        ulong channelId,
        string? text,
        TimeProvider? time,
        MessageReference? messageReference = null,
        Embed? embed = null,
        List<Attachment>? attachments = null
    ){
        Mentions mentions = MentionsHelper.ExtractMentions(text, embed);
        Message message = new(
            id, 
            userId, 
            channelId, 
            text,
            time?.GetUtcNow() ?? DateTimeOffset.Now, 
            messageReference,
            embed, 
            attachments: attachments,
            mentions: mentions
        );

        MessageSentEvent messageSent = MessageSentEvent.From(message);
        message.AddDomainEvent(messageSent);
        return message;
    }
}