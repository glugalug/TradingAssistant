using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    internal class AsyncToSyncWrapper
    {
        private static async Task wrapAsyncResultForSyncWrapping<T>(Func<Task<T>> func, AsyncResultHodler<T> hodler)
        {
            try
            {
                hodler.result = await func.Invoke();
            }
            catch (Exception ex)
            {
                hodler.e = ex;
            } finally
            {
                hodler.completion_event.Set();
            }
        }

        // Wrapper for an async result with a completion event to make it easy to wrap as sync.
        internal class AsyncResultHodler<T>
        {
            public T? result;
            public Exception? e;
            public ManualResetEvent completion_event = new ManualResetEvent(false);
        }

        public static T callAsyncAsSync<T>(Func<Task<T>> func)
        {
            AsyncResultHodler<T> hodler = new();
            Task.Run(async () => wrapAsyncResultForSyncWrapping<T>(func, hodler));
            hodler.completion_event.WaitOne();
            if (hodler.e != null) { throw hodler.e; }
            return hodler.result;
        }
    }
}
