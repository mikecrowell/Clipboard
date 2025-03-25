using FieldTool.ClipboardLookup.Controllers;
using FieldTool.ClipboardLookup.Logging;
using FieldTool.Constants.Helpers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace FieldTool.ClipboardLookup.Filters
{
    public class ErrorHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log(actionExecutedContext);
            base.OnException(actionExecutedContext);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Log(actionExecutedContext);
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }

        private void Log(HttpActionExecutedContext context)
        {
            var e = ExceptionHelper.Innermost(context.Exception);
            var c = context.ActionContext.ControllerContext.Controller as BaseLoggingController;
            (c?.Logger ?? new Logger()).LogError(e, $"Error {context?.Response?.StatusCode} with request: {context?.Request?.RequestUri?.AbsoluteUri}");
        }
    }
}