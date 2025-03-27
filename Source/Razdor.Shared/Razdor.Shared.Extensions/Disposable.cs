using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razdor.Shared
{
    public abstract class Disposable : IDisposable
    {
        protected bool Disposed { get; private set; }

        public void ThrowIfDisposed() 
        {
            if (Disposed)
            { 
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposable() 
        {
            Dispose(false);
        }

    }
}
