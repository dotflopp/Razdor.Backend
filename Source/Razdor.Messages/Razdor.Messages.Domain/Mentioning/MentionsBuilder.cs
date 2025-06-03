namespace Razdor.Messages.Domain.Mentioning;

public class MentionsBuilder 
{
    private List<MentionedUser>? _users = null;
    private List<MentionedChannel>? _channels = null;
    private List<MentionedRole>? _roles = null;
    private bool _mentionedEveryone = false;

    public MentionsBuilder HasEveryoneMention(bool hasMention = true)
    {
        _mentionedEveryone = hasMention;
        return this;
    }

    public MentionsBuilder WithUserMention(MentionedUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        
        _users ??= new List<MentionedUser>();
        _users.Add(user);
        return this;
    }

    public MentionsBuilder WithChannelMention(MentionedChannel channel)
    {
        ArgumentNullException.ThrowIfNull(channel);
        
        _channels ??= new List<MentionedChannel>();
        _channels.Add(channel);
        return this;
    }

    public MentionsBuilder WithRoleMention(MentionedRole role)
    {
        ArgumentNullException.ThrowIfNull(role);
        
        _roles ??= new List<MentionedRole>();
        _roles.Add(role);
        return this;
    }

    public Mentions Build()
    {
        return new Mentions(_mentionedEveryone, _users, _channels, _roles);
    }
}