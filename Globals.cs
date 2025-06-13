using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingAssistant
{
    internal static class Globals
    {
        static public JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Include,
            Error = JsonErrorEventHandler,
        };
        private static void JsonErrorEventHandler(object? sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            throw new InvalidDataException(string.Format(
                "JSON serialization or deserialization error, sender: `{0}` error: `{1}`", sender, e));
        }

        public static string getAppDataFolder()
        {
            return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TradingAssistant");
        }
    }
}
