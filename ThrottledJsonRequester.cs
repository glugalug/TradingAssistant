using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingAssistant
{
    internal class ThrottledHttpRequester : IDisposable
    {
        public struct Options
        {
            // Minimum time between requests, measured from start to start.
            public TimeSpan minRequestInterval;
        }

        public ThrottledHttpRequester(Options options)
        {
            options_ = options;
        }
        // Minimum time between requests, measured from start to start.
        public TimeSpan minRequestInterval { get { return options_.minRequestInterval; } set { options_.minRequestInterval = value; } }

        public delegate void ProcessHttpResponseDelegate(HttpResponseMessage response_message);
        public async Task<string> GetAsync(HttpRequestMessage message, ProcessHttpResponseDelegate? preParser = null)
        {
            try
            {
                using (var startMutexLock = new ReleaseableMutexLock(start_mutex_))
                {
                    DateTime minStart = lastRequestStart_.Add(minRequestInterval);
                    TimeSpan remaining = minStart.Subtract(DateTime.Now);
                    if (remaining > TimeSpan.Zero)
                    {
                        lastRequestStart_ = minStart;
                        startMutexLock.Release();
                        Console.WriteLine("Throttling, before request '{0}', {1} remains", message, remaining);
                        await Task.Delay(remaining);
                    }
                }
                using (var startMutexLock = new ReleaseableMutexLock(start_mutex_))
                {
                    lastRequestStart_ = DateTime.Now;
                }
            }
            finally
            {
                //start_mutex_.ReleaseMutex();
            }
            Console.WriteLine(string.Format("Starting request: '{0}'", message));
            using HttpResponseMessage httpResponse = await client.SendAsync(message);
            if (preParser != null)
            {
                preParser.Invoke(httpResponse);
            }
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException(string.Format(
                    "Got http error response from request for '{0}' with message '{1}'", message, httpResponse.ReasonPhrase),
                    null, httpResponse.StatusCode);
            }
            string responseText = await httpResponse.Content.ReadAsStringAsync();
            if (Properties.Settings.Default.debugHttpResponses)
            {
                Console.WriteLine(string.Format("response text: '''\n{0}\n'''", responseText));
            }
            return responseText;
        }

        private class AsyncResultHodler<T>
        {
            public T? result;
            public Exception? e;
            public ManualResetEvent completion_event = new ManualResetEvent(false);
        }

        private async Task internalGetAsyncForSyncWrapping(HttpRequestMessage message, AsyncResultHodler<string> resultHodler)
        {
            try
            {
                resultHodler.result = await GetAsync(message);
            }
            catch (Exception ex)
            {
                resultHodler.e = ex;
            }
            finally
            {
                resultHodler.completion_event.Set();
            }
        }

        public string GetSync(HttpRequestMessage message)
        {
            AsyncResultHodler<string> resultHodler = new();
            Task.Run(async () => internalGetAsyncForSyncWrapping(message, resultHodler));
            resultHodler.completion_event.WaitOne();
            if (resultHodler.e != null) { throw resultHodler.e; }
            return resultHodler.result;
        }

        public async Task<T> GetAndParseJsonAsync<T>(
            HttpRequestMessage message,
            JsonSerializerSettings? jsonSettings=null,
            ProcessHttpResponseDelegate? preParser = null)
        {
            string jsonText = await GetAsync(message, preParser);
            var response = JsonConvert.DeserializeObject<T>(jsonText, jsonSettings ?? defaultSerializerSettings);
            if (response == null)
            {
                throw new Exception(string.Format("Failed to parse json response: '{0}'", jsonText));
            }
            return response;
        }

        private async Task internalGetJsonAsyncForSyncWrapping<T>(
            HttpRequestMessage message,
            AsyncResultHodler<T> resultHodler,
            JsonSerializerSettings jsonSettings=null,
            ProcessHttpResponseDelegate? preParser = null)
        {
            try
            {
                resultHodler.result = await GetAndParseJsonAsync<T>(message, jsonSettings, preParser);
            }
            catch (Exception ex)
            {
                resultHodler.e = ex;
            }
            finally
            {
                resultHodler.completion_event.Set();
            }
        }

        public T GetAndParseJsonSync<T>(HttpRequestMessage message, 
                                         JsonSerializerSettings? jsonSettings = null, 
                                         ProcessHttpResponseDelegate? preParser = null)
        {
            AsyncResultHodler<T> hodler = new();
            Task.Run(async () => internalGetJsonAsyncForSyncWrapping<T>(message, hodler, jsonSettings, preParser));
            hodler.completion_event.WaitOne();
            if (hodler.e != null) { throw hodler.e; }
            return hodler.result;
        }

        public JsonSerializerSettings defaultSerializerSettings { get { return defaultserializerSettings_; } set { defaultserializerSettings_ = value; } }
        private JsonSerializerSettings defaultserializerSettings_ = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Include,
            Error = JsonErrorEventHandler,
        };
        private static void JsonErrorEventHandler(object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            throw new InvalidDataException(string.Format("JSON serialization or deserialization error, sender: `{0}` error: `{1}`", sender, e));
        }

        private HttpClient client = new HttpClient();
        private Options options_;
        private DateTime lastRequestStart_ = DateTime.MinValue;
        private Mutex start_mutex_ = new Mutex(false);
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                client.Dispose();

                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ThrottledHttpRequester()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
