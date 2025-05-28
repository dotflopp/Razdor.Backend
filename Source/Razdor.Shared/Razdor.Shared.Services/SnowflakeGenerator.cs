using Razdor.Shared.Domain;

namespace Razdor.Shared.Module;

/// <summary>
///     UNIX MILLISECONDS sizeof(ulong) - WorkerIdSize - IncrementSize | worker ID WorkerIdSize | incremented IncrementSize
/// </summary>
public sealed class SnowflakeGenerator
{
    private const int WorkerIdSizeBits = 4;
    private const int IncrementSizeBits = 18;
    public const byte MaxWorkerId = 0b1 << (WorkerIdSizeBits + 1 - 1);
    public const uint MaxIncrement = 0b1 << (IncrementSizeBits + 1 - 1);
    private readonly DateTime _startTime;
    private readonly byte _workerId;

    private Determinant _determinant;

    public SnowflakeGenerator(byte workerId, DateTime startTime)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(workerId, MaxWorkerId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startTime, DateTime.UtcNow);

        _determinant = new Determinant(0, 0);
        _workerId = workerId;
        _startTime = startTime;
    }

    public ulong Next()
    {
        var determinant = SelectDeterminant();
        return CreateSnowflake(determinant);
    }

    private ulong GetNowMs()
    {
        return (ulong)(DateTime.UtcNow - _startTime).TotalMilliseconds;
    }

    private ulong CreateSnowflake(Determinant determinant)
    {
        var billet = determinant.TimeMs << (WorkerIdSizeBits + IncrementSizeBits);
        billet |= (ulong)_workerId << IncrementSizeBits;
        billet |= determinant.Increment & MaxIncrement;

        return billet;
    }

    private Determinant SelectDeterminant()
    {
        var newValue = new Determinant(0, 0);
        while (true)
        {
            var nowMs = GetNowMs();
            var current = _determinant;

            if (current.TimeMs < nowMs) 
            {
                newValue.TimeMs = nowMs;
                newValue.Increment = 0;
            }
            else
            {
                newValue.TimeMs = current.TimeMs;
                newValue.Increment = current.Increment + 1;

                if (newValue.Increment > MaxIncrement)
                    continue;
            }

            var updated = Interlocked.CompareExchange(ref _determinant, newValue, current);
            if (current == updated) return newValue;
        }
    }

    private class Determinant(ulong timeMs, uint increment)
    {
        public uint Increment = increment;
        public ulong TimeMs = timeMs;
    }
}