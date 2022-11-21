using System.Net;
using AzureBlobStorage.API.Infrastructure.ActionResults;
using AzureBlobStorage.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AzureBlobStorage.API.Infrastructure.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<HttpGlobalExceptionFilter> _logger;

    public HttpGlobalExceptionFilter(
        IWebHostEnvironment webHostEnvironment,
        ILogger<HttpGlobalExceptionFilter> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            context.Exception.Message);

        var exceptionType = context.Exception.GetType();


        var json = new JsonErrorResponse
        {
            Messages = new[] { context.Exception.Message }
        };

        if (exceptionType.BaseType == typeof(ResourceNotFoundServiceException)
            || exceptionType.BaseType == typeof(InvalidRequestException)
            )
        {
            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception;
            }

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        context.ExceptionHandled = true;
    }
}