using FieldTool.ClipboardLookup.Controllers;
using FieldTool.Constants.Logging;
using System.Web.Http.Filters;

namespace FieldTool.ClipboardLookup.Logging
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public ILogger Logger { get; set; }

        public UnhandledExceptionFilter(ILogger logger)
        {
            this.Logger = logger;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var c = actionExecutedContext?.ActionContext?.ControllerContext?.Controller as BaseLoggingController;
            (c.Logger ?? this.Logger).LogError(actionExecutedContext.Exception, "UnhandledException");
        }
    }
}