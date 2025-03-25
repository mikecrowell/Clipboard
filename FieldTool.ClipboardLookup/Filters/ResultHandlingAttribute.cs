using FieldTool.ClipboardLookup.Controllers;
using Newtonsoft.Json;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FieldTool.ClipboardLookup.Filters
{
    public class ResultHandlingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var method = actionContext?.Request?.Method;
            var uri = actionContext?.Request?.RequestUri.AbsoluteUri;
            var msg = $"Request: {method} [{uri}]";

            var c = actionContext.ControllerContext.Controller as BaseLoggingController;
            c.Logger.LogMessage(msg);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var method = actionExecutedContext?.Request?.Method;
            var status = actionExecutedContext?.Response?.StatusCode ?? 0;
            var uri = actionExecutedContext?.ActionContext?.Request?.RequestUri.AbsoluteUri;
            var reason = actionExecutedContext?.Response?.ReasonPhrase;

            var msg = $"Response: [{status}] for {method} request [{uri}]";
            var c = actionExecutedContext?.ActionContext?.ControllerContext?.Controller as BaseLoggingController;
            try
            {
                var o = new
                {
                    method = method,
                    code = (int)status,
                    description = status,
                    reason = JsonConvert.DeserializeObject(reason),
                    uri = uri
                };
                c.Logger.LogMessage(o, msg);
            }
            catch (Exception)
            {
                var o = new
                {
                    method = method,
                    code = (int)status,
                    description = status,
                    reason = reason,
                    uri = uri
                };
                c.Logger.LogMessage(o, msg);
            }
        }
    }
}