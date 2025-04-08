using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Razdor.Identity.Domain.DomainEvents;
using Razdor.Shared.Domain;

namespace Razdor.Identity.Domain;

public record UserAccountId(ulong Value) : ISnowflakeId;

public class UserAccount: BaseEntity
{
    public const int MaxIdentityNameLength = 50;
    
    internal UserAccount(
        ulong id, 
        string identityName, 
        string email, 
        string? hashedPassword, 
        DateTimeOffset credentialsChangeDate
    ) : base(id) 
    {
        IdentityName = identityName;
        Email = email;
        HashedPassword = hashedPassword;
        CredentialsChangeDate = credentialsChangeDate;
    }
    
    public string IdentityName { get; private set; }    
    public string Email { get; private set; }
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

    public static UserAccount RegisterNew(ulong id, string identityName, string email, string? passwordHash)
    {
        ArgumentNullException.ThrowIfNull(identityName);
        
        UserAccount account = new UserAccount(
            id, 
            identityName, 
            email,
            passwordHash,
            DateTimeOffset.UtcNow
        );
        
        account.AddDomainEvent(new UserAccountCreated(account));
        return account;
    }
}
