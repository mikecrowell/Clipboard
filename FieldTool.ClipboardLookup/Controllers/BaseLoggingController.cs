using FieldTool.ClipboardLookup.Filters;
using FieldTool.Constants.Logging;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers
{
    [ResultHandling]
    [ErrorHandling]
    public abstract class BaseLoggingController : ApiController
    {
        public ILogger Logger { get; private set; }

        public BaseLoggingController(ILogger logger)
        {
            Logger = logger;
        }
    }
}