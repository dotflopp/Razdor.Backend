using Razdor.Shared.Extensions;

namespace Razdor.Identity.Module.Commands;

public abstract class AuthenticationError(
    ulong code,
    string message
) : BaseError(code, message)
{
    public static readonly InvalidPasswordOrEmailError InvalidPasswordOrEmailError = 
        new ("Invalid Password or Email");
    
    public static readonly UserAlreadyExistsError UserAlreadyExistsError = 
        new ("User already exists");
}

public class InvalidPasswordOrEmailError(
    string message    
) : AuthenticationError(1, message);

public class UserAlreadyExistsError(
    string message    
): AuthenticationError(2, message);