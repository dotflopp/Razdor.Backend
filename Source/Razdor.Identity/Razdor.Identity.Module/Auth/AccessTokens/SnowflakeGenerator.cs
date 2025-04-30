namespace Razdor.Identity.Module.Auth.AccessTokens;
/// <summary>
/// UNIX MILLISECONDS sizeof(ulong) - WorkerIdSize - IncrementSize | worker ID WorkerIdSize | incremented IncrementSize 
/// </summary>
public class SnowflakeGenerator
{
    private class Determinant(ulong timeMs, uint increment)
    {
        public ulong TimeMs = timeMs;
        public uint Increment = increment;
    };

    private const int WorkerIdSizeBits = 4;
    private const int IncrementSizeBits = 18;
    public const byte MaxWorkerId = 0b1 << (WorkerIdSizeBits + 1) - 1;
    public const uint MaxIncrement = 0b1 << (IncrementSizeBits + 1) - 1;
    
    private Determinant _determinant;
    private byte _workerId;
    private DateTime _startTime;
    
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
        Determinant determinant = SelectDeterminant(); 
        return CreateSnowflake(determinant);
    }
    
    private ulong GetNowMs() => (ulong)(DateTime.UtcNow - _startTime).TotalMilliseconds;

    private ulong CreateSnowflake(Determinant determinant)
    {
        ulong billet = determinant.TimeMs << (WorkerIdSizeBits + IncrementSizeBits);
        billet |= (ulong)_workerId << IncrementSizeBits;
        billet |= (determinant.Increment & MaxIncrement);
        
        return billet;
    }
    
    private Determinant SelectDeterminant()
    {
        Determinant newValue = new Determinant(0, 0);
        while(true)
        {
            ulong nowMs = GetNowMs();
            Determinant current = _determinant;
            
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
            
            Determinant updated = Interlocked.CompareExchange(ref _determinant, newValue, current);
            if (current == updated)
            {
                return newValue;
            }
        }
    }
}
