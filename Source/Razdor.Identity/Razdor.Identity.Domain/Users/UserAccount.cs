using System.ComponentModel.DataAnnotations;
using Razdor.Identity.Domain.Users.Events;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users;

public class UserAccount : BaseSnowflakeEntity, ISnowflakeEntity, IEntity<ulong>, IAggregateRoot
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
        UserCommunicationStatus status,
        string? description
    ) : base(id)
    {
        IdentityName = identityName;
        _nickname = nickname;
        Email = email;
        HashedPassword = hashedPassword;
        CredentialsChangeDate = credentialsChangeDate;
        IsOnline = isOnline;
        Status = status;
        Description = description;
    }

    public string IdentityName { get; }
    public string Email { get; private set; }
    public string Nickname => _nickname ?? IdentityName;
    public string? Avatar { get; private set; }
    public string? HashedPassword { get; private set; }
    public bool IsOnline { get; private set; }

    /// <summary>
    ///     Дата и время изменения пароля или логина.
    /// </summary>
    public DateTimeOffset CredentialsChangeDate { get; private set; }
    
    public UserCommunicationStatus Status { get; private set; }
    public UserCommunicationStatus CurrentStatus => 
        !IsOnline
            ? UserCommunicationStatus.Offline 
            : Status == UserCommunicationStatus.Invisible
                ? UserCommunicationStatus.Online 
                : Status;
    
    public string? Description { get; private set; }

    
    /// <summary>
    ///     Установит новый пароль пользователю
    /// </summary>
    /// <param name="newHashedPassword"></param>
    /// <param name="time"></param>
    public void ChangePassword(string newHashedPassword, TimeProvider? time = null)
    {
        var oldPassword = HashedPassword;
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
        TimeProvider? time = null
    )
    {
        ArgumentNullException.ThrowIfNull(identityName);
        ArgumentNullException.ThrowIfNull(email);

        var credentialsChangeDate = time?.GetUtcNow() ?? DateTimeOffset.UtcNow;

        var account = new UserAccount(
            id,
            identityName,
            email,
            nickname,
            avatar,
            hashedPassword,
            credentialsChangeDate,
            false,
            UserCommunicationStatus.Online,
            null
        );

        account.AddDomainEvent(new UserAccountCreated(account));
        return account;
    }
}