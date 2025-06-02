using Razdor.Identity.Domain.Users.Events;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users;

public class UserAccount : BaseSnowflakeEntity, IEntity<ulong>, IAggregateRoot
{
    public const int MaxIdentityNameLength = 50;
    public const int MaxNicknameLength = MaxIdentityNameLength;
    public const int MaxDescriptionLength = 300;

    private readonly string? _nickname;

    internal UserAccount(
        ulong id,
        string identityName,
        string email,
        string? nickname,
        string? avatar,
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
    public string? Avatar { get; private set; }
    public string? HashedPassword { get; private set; }
    public bool IsOnline { get; }

    /// <summary>
    ///     Дата и время изменения пароля или логина.
    /// </summary>
    public DateTimeOffset CredentialsChangeDate { get; private set; }

    public SelectedCommunicationStatus SelectedStatus
    {
        get;
        set
        {
            if (IsOnline)
            {
                AddDomainEvent(new UserCommunicationStatusChanged(
                    Id,
                    DisplayedStatus,
                    (DisplayedCommunicationStatus)value
                ));
            }

            field = value;
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
            AddDomainEvent(new UserPasswordChanged(this, oldPassword));
    }

    public static UserAccount RegisterNew(
        ulong id,
        string identityName,
        string email,
        string? nickname,
        string? avatar,
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
}