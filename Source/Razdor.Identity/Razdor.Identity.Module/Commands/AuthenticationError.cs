using Razdor.Shared.Extensions;

namespace Razdor.Identity.Module.Commands;

public class AuthenticationError(
    string Message
) : BaseError(0, Message)
{
    public static AuthenticationError InvalidPasswordOrEmailError = new AuthenticationError("Invalid Password or Email");
}