using FieldTool.Constants.Logging;
using Loggly;
using Newtonsoft.Json;
using System;

namespace FieldTool.ClipboardLookup.Logging
{
    public class Logger : ILogger
    {
        private ILogglyClient log = new LogglyClient();
        private long trace = DateTime.Now.Ticks;

        public string GetTrace()
        {
            return trace.ToString();
        }

        public void LogMessage(string message)
        {
            DoLog(null, message, false);
        }

        public void LogMessage(object o, string message)
        {
            DoLog(o, message, false);
        }

        public void LogError(object o)
        {
            DoLog(o, null, true);
        }

        public void LogError(object o, string message)
        {
            DoLog(o, message, true);
        }

        private void DoLog(object o, string message, bool isError = false)
        {
            var logEvent = new LogglyEvent();
            logEvent.Data.Add("trace", GetTrace());

            var obj = (o == null) ? null : o;
            try
            {
                if (!String.IsNullOrEmpty(message))
                {
                    logEvent.Data.Add("message", message);
                }

                if (obj is Exception || isError)
                {
                    logEvent.Options.Tags.Add(new Loggly.Config.SimpleTag { Value = "exception" });
                }

                var canSerialize = JsonConvert.SerializeObject(obj);
                // if it gets here, the object will serialize for Loggly.
                logEvent.Data.Add("context", obj);
            }
            catch (Exception)
            {
                logEvent.Options.Tags.Add(new Loggly.Config.SimpleTag { Value = "error" });
                logEvent.Data.Add("context", new
                {
                    error = "Could not serialize object",
                    obj = ((o == null) ? "null" : o.ToString())
                });
            }

            log.Log(logEvent);
        }
    }
}