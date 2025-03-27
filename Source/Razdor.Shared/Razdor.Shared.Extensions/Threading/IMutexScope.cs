namespace Razdor.Shared.Threading;

public interface IMutexScope<T> : IDisposable
{
    T Mutex { get; }
}