using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    internal class ReleaseableMutexLock : IDisposable
    {
        public ReleaseableMutexLock(Mutex mu, int millisecondsTimeout = -1)
        {
            mu_ = mu;
            mu_.WaitOne(millisecondsTimeout);
        }

        public void Release()
        {
            if (lockHeld_)
            {
                mu_.ReleaseMutex();
                lockHeld_ = false;
            }
        }

        private bool lockHeld_ = true;

        private Mutex mu_;
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Release();
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ReleaseableMutexLock()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
