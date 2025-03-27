namespace Razdor.Shared.Threading;

public sealed class MutexScope(Mutex mutex) : Disposable, IMutexScope<Mutex>
{
    public Mutex Mutex { get; } = mutex;

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        if (disposing)
        {
            Mutex.ReleaseMutex();
        }
    }
}