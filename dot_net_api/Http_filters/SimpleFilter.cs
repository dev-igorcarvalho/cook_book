using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace dot_net_api.Http_filters
{
    public class SimpleFilter : IActionFilter
    {
        private readonly ILogger<SimpleFilter> _logger;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request.ToString();
            _logger.LogDebug("Logando antes da execução da ação");
            _logger.LogDebug(request);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response.ToString();
            _logger.LogDebug("Logando depois da execução da ação");
            _logger.LogDebug(response);
        }

    }
}