using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Identity.Module.Auth.Commands.Exceptions;

public class InvalidPasswordOrEmailException(
    string? message = null, Exception? innerException = null
) : RazdorException(ErrorCode.InvalidPasswordOrEmail, message, innerException);