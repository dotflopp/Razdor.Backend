using System.Diagnostics.CodeAnalysis;

namespace Razdor.Shared.Extensions;

public readonly struct ValueResult<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;
    private readonly bool _isSuccess;

    private ValueResult(TValue? value, TError? error, bool isSuccess)
    {
        _value = value;
        _error = error;
        _isSuccess = isSuccess;
    }

    public bool TrySuccess(
        [MaybeNullWhen(false)] out TValue value,
        [MaybeNullWhen(true)] out TError error)
    {
        value = _value;
        error = _error;
        return _isSuccess;
    }

    public bool TryFailure(
        [MaybeNullWhen(true)] out TValue value,
        [MaybeNullWhen(false)] out TError error)
    {
        value = _value;
        error = _error;
        return !_isSuccess;
    }

    public static ValueResult<TValue, TError> Ok(TValue success)
    {
        return new ValueResult<TValue, TError>(success, default, true);
    }

    public static ValueResult<TValue, TError> Fail(TError error)
    {
        return new ValueResult<TValue, TError>(default, error, false);
    }

    public static implicit operator ValueResult<TValue, TError>(TValue success)
    {
        return Ok(success);
    }

    public static implicit operator ValueResult<TValue, TError>(TError error)
    {
        return Fail(error);
    }

    public override string ToString()
    {
        return TrySuccess(out TValue? value, out TError? error)
            ? $"Success {{ Value = {value}; }}"
            : $"Error {{ Value = {error}; }}";
    }
}