namespace Razdor.Shared.Extensions;

public abstract class Disposable : IDisposable
{
    protected bool Disposed { get; private set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void ThrowIfDisposed()
    {
        if (Disposed) throw new ObjectDisposedException(GetType().Name);
    }

    protected virtual void Dispose(bool disposing)
    {
        Disposed = true;
    }

    ~Disposable()
    {
        Dispose(false);
    }
}