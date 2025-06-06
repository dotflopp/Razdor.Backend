﻿using Razdor.Shared.Domain.Exceptions;

namespace Razdor.Shared.Module.Exceptions;

public sealed class UnauthenticatedException(
    string? message = null,
    Exception? innerException = null
) : RazdorException(ErrorCode.Unauthorized, message, innerException);