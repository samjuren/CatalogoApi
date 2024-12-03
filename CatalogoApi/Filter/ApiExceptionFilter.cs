using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoApi.Filter;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu um erro não tratado");
        
        context.Result = new ObjectResult("Ocorreu um problema ao tratar sua solicitação")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}