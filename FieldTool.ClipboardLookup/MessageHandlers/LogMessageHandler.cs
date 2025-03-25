using FieldTool.Constants.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.MessageHandlers
{
    public class LogMessageHandler : DelegatingHandler
    {
        public ILogger Logger { get; set; }

        public LogMessageHandler(ILogger logger)
        {
            this.Logger = logger;
        }

        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string messageId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);

            await LogRequestAsync(messageId, request.RequestUri.ToString());

            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                byte[] responseData = await response.Content.ReadAsByteArrayAsync();
                await LogResponseAsync(messageId, request.RequestUri.ToString(), BuildResponseStatusLogString(response.StatusCode), System.Text.Encoding.UTF8.GetString(responseData));
            }
            else
            {
                await LogResponseAsync(messageId, request.RequestUri.ToString(), BuildResponseStatusLogString(response.StatusCode), response.ReasonPhrase);
            }

            return response;
        }

        protected async Task LogRequestAsync(string messageId, string message)
        {
            await Task.Run(() => LogMessage("{0} - Request: {1}", messageId, message));
        }

        protected async Task LogResponseAsync(string messageId, string requestUri, string status, string message)
        {
            await Task.Run(() => LogMessage("{0} - Response: {1} - {2}", messageId, status, message));
        }

        private void LogMessage(string messageFormat, params object[] args)
        {
            this.Logger.LogMessage(string.Format(messageFormat, args));
        }

        private string BuildResponseStatusLogString(System.Net.HttpStatusCode statusCode)
        {
            return string.Format("[{0} - {1}]", ((int)statusCode).ToString(), statusCode.ToString());
        }
    }
}