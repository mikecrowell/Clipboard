using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Results;

namespace FieldTool.ClipboardLookup.MessageHandlers
{
    public class SecurityMessageHandler : DelegatingHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            bool authorized = false;

            var token = request.GetQueryNameValuePairs().FirstOrDefault(x => x.Key.ToLower() == DataHelper.GetPropertyName<SecurityToken>(st => st.Token).ToLower());
            authorized = token.Value == ConfigurationManager.AppSettings["clientPasscode"];

            return authorized ? await base.SendAsync(request, cancellationToken) : await (new UnauthorizedResult(new List<AuthenticationHeaderValue>(), request)).ExecuteAsync(cancellationToken);
        }

        private static async System.Threading.Tasks.Task<bool> CheckFormDataPasscode(HttpContent content)
        {
            NameValueCollection requestFormData = await content.ReadAsFormDataAsync();
            return requestFormData.ContainsExpectedValue(DataHelper.GetPropertyName<SecurityToken>(x => x.Token), ConfigurationManager.AppSettings["clientPasscode"]);
        }
    }
}