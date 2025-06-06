using Razdor.Identity.Domain.Events;
using Razdor.Identity.Domain.Users.Events;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users;

public class UserAccount : BaseSnowflakeEntity, IEntity<ulong>, IAggregateRoot
{
    public const int MaxIdentityNameLength = 50;
    public const int MaxNicknameLength = MaxIdentityNameLength;
    public const int MaxDescriptionLength = 300;
    private string? _nickname;
    
    private UserChangedEvent? _changes;
    
    /// <summary>
    /// EF constructor
    /// </summary>
    private UserAccount():this(0, null!, null!, null, null, null, default, false, default, null, default)
    { }
    
    internal UserAccount(
        ulong id,
        string identityName,
        string email,
        string? nickname,
        MediaFileMeta? avatar,
        string? hashedPassword,
        DateTimeOffset credentialsChangeDate,
        bool isOnline,
        SelectedCommunicationStatus selectedStatus,
        string? description,
        DateTimeOffset registrationDate
    ) : base(id)
    {
        IdentityName = identityName;
        _nickname = nickname;
        Email = email;
        HashedPassword = hashedPassword;
        CredentialsChangeDate = credentialsChangeDate;
        IsOnline = isOnline;
        SelectedStatus = selectedStatus;
        Description = description;
        RegistrationDate = registrationDate;
    }

    public string IdentityName { get; }
    public string Email { get; private set; }
    public string Nickname => _nickname ?? IdentityName;
    public MediaFileMeta? Avatar
    {
        get => field;
        set
        {
            field = value;
            CollectChanges(UserProperties.Avatar);
        }
    }
    public string? HashedPassword { get; private set; }
    public bool IsOnline { 
        get => field;
        set
        {
            var before = DisplayedStatus;
            field = value;
                      
            if (before != DisplayedStatus)
                CollectChanges(UserProperties.DisplayedStatus);
            
            CollectChanges(UserProperties.IsOnline);
        }
    }

    /// <summary>
    ///     Дата и время изменения пароля или логина.
    /// </summary>
    public DateTimeOffset CredentialsChangeDate { get; private set; }

    public SelectedCommunicationStatus SelectedStatus
    {
        get;
        set
        {
            var before = DisplayedStatus;
            field = value;
            
            if (before != DisplayedStatus)
                CollectChanges(UserProperties.DisplayedStatus);

            CollectChanges(UserProperties.SelectedStatus);
        }
    }

    public DisplayedCommunicationStatus DisplayedStatus =>
        !IsOnline
            ? DisplayedCommunicationStatus.Offline
            : (DisplayedCommunicationStatus)SelectedStatus;

    public string? Description { get; private set; }

    public DateTimeOffset RegistrationDate { get; private set; }

    /// <summary>
    ///     Установка нового пароля для пользователя
    /// </summary>
    /// <param name="newHashedPassword"></param>
    /// <param name="time"></param>
    public void ChangePasswordHash(string newHashedPassword, TimeProvider? time = null)
    {
        string? oldPassword = HashedPassword;
        HashedPassword = newHashedPassword;
        CredentialsChangeDate = time?.GetUtcNow() ?? DateTimeOffset.UtcNow;

        if (!DomainEvents.Any(x => x is UserAccountCreated))
            CollectChanges(UserProperties.CredentialsChangeDate);
    }

    public static UserAccount RegisterNew(
        ulong id,
        string identityName,
        string email,
        string? nickname,
        MediaFileMeta? avatar,
        string? hashedPassword,
        IUsersCounter counter,
        TimeProvider? time = null
    )
    {
        ArgumentNullException.ThrowIfNull(identityName);
        ArgumentNullException.ThrowIfNull(email);

        DateTimeOffset credentialsChangeDate = time?.GetUtcNow() ?? DateTimeOffset.UtcNow;
        DateTimeOffset registrationDate = time?.GetUtcNow() ?? DateTimeOffset.UtcNow;

        var account = new UserAccount(
            id,
            identityName,
            email,
            nickname,
            avatar,
            hashedPassword,
            credentialsChangeDate,
            false,
            SelectedCommunicationStatus.Online,
            null,
            registrationDate
        );

        account.AddDomainEvent(new UserAccountCreated(account));
        return account;
    }


    /// <summary>
    /// Вызывать после присваивания значения полю.
    /// </summary>
    private void CollectChanges(UserProperties properties)
    {
        if (_changes is null)
        {
            _changes = new UserChangedEvent(Id);
            AddDomainEvent(_changes);
        }
        
        _changes.UserProperties |= properties;
        
        switch (properties)
        {
            case UserProperties.DisplayedStatus:
                _changes.DisplayedStatus = DisplayedStatus;
                break;
            case UserProperties.Nickname:
                _changes.Nickname = _nickname;
                break;
            case UserProperties.Avatar:
                _changes.Avatar = Avatar;
                break;
            case UserProperties.CredentialsChangeDate:
                _changes.CredentialsChangeDate = DateTimeOffset.UtcNow;
                break;
            case UserProperties.IsOnline:
                _changes.IsOnline = IsOnline;
                break;
            case UserProperties.SelectedStatus:
                _changes.SelectedStatus = SelectedStatus;
                break;
            case UserProperties.Description:
                _changes.Description = Description;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(properties), properties, null);
        }
    }
}