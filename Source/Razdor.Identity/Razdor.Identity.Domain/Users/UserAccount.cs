using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain.Users;

public class UserAccount: BaseEntity
{
    public const int MaxIdentityNameLength = 50;
    private string? _nickname;
    
    internal UserAccount(
        ulong id, 
        string identityName, 
        string email, 
        string? nickname,
        string avatar,
        string? hashedPassword, 
        DateTimeOffset credentialsChangeDate
    ) : base(id) 
    {
        IdentityName = identityName;
        _nickname = nickname;
        Email = email;
        HashedPassword = hashedPassword;
        CredentialsChangeDate = credentialsChangeDate;
    }
    
    public string IdentityName { get; private set; }
    public string Email { get; private set; }
    public string Nickname => _nickname ?? IdentityName;
    public string? Avatar { get; private set; }
    public string? HashedPassword { get; private set; }
    
    /// <summary>
    /// Дата и время изменения пароля или логина.
    /// </summary>
    [Required]
    public DateTimeOffset CredentialsChangeDate { get; private set; }

    public void ChangePassword(string newHashedPassword)
    {
        HashedPassword = newHashedPassword;
        CredentialsChangeDate = DateTimeOffset.UtcNow;
    }

    public static UserAccount RegisterNew(
        ulong id,
        string identityName,
        string email,
        string? nickname,
        string avatar,
        string? hashedPassword,
        TimeProvider? timeProvider = null
    ){

        ArgumentNullException.ThrowIfNull(identityName);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(avatar);

        DateTimeOffset credentialsChangeDate = timeProvider?.GetUtcNow() ?? DateTimeOffset.UtcNow;

        UserAccount account = new UserAccount(
            id, 
            identityName, 
            email,
            nickname,
            avatar,
            hashedPassword,
            credentialsChangeDate
        );
        
        account.AddDomainEvent(new UserAccountCreated(account));
        return account;
    }
}
