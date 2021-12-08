using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace dot_net_api.Http_filters
{
    public class SimpleFilter : IActionFilter
    {
        private readonly ILogger<SimpleFilter> _logger;
        public SimpleFilter(ILogger<SimpleFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Logando antes da execução da ação");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Logando depois da execução da ação");
        }

    }
}