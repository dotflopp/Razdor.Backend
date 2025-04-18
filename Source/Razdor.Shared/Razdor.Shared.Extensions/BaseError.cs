﻿namespace Razdor.Shared.Extensions;

public abstract class BaseError (ulong code, string message)
{
    public ulong Code { get; protected set; } = code;
    public string Message { get; protected set; } = message;
}