using System.Collections.ObjectModel;

namespace Razdor.Messaging.Domain.Mentioning;

public class Mentions
{
    private IReadOnlyCollection<MentionedUser>? _users;
    private IReadOnlyCollection<MentionedRole>? _roles;
    private IReadOnlyCollection<MentionedChannel>? _channels;

    public Mentions() : this(false, null, null, null)
    { }

    public Mentions(
        bool mentionedEveryone,
        IReadOnlyCollection<MentionedUser>? users = null,
        IReadOnlyCollection<MentionedChannel>? channels = null,
        IReadOnlyCollection<MentionedRole>? roles = null
    ){
        MentionedEveryone = mentionedEveryone;
    }
    
    public bool MentionedEveryone { get; private set; }
    public IReadOnlyCollection<MentionedUser> Users => _users ?? ReadOnlyCollection<MentionedUser>.Empty;
    public IReadOnlyCollection<MentionedChannel> Channels => _channels ?? ReadOnlyCollection<MentionedChannel>.Empty;
    public IReadOnlyCollection<MentionedRole> Roles => _roles ?? ReadOnlyCollection<MentionedRole>.Empty;
}