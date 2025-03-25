using FieldTool.ClipboardLookup.Logging;
using FieldTool.Constants.Helpers;
using System;
using System.Web.Http;

namespace FieldTool.ClipboardLookup
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new LogMessageHandler(new Logger()));
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new SecurityMessageHandler());
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new LinkedResourceMessageHandler());
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var logger = new Logger();
            Exception ex = Server.GetLastError();
            var inner = ExceptionHelper.Innermost(ex);
            logger.LogError(inner, "UnhandledException:" + inner.Message);
        }
    }
}