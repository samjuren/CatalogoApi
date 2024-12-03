using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoApi.Filter;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //antes da Action
        _logger.LogInformation("Executando -> OnActionExecuting");
        _logger.LogInformation("###########################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        _logger.LogInformation("###########################################");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //depois da Action
        _logger.LogInformation("Executando -> OnActionExecuted");
        _logger.LogInformation("###########################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("###########################################");
    }
}