using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Aktitic.HrProject.Api.Configuration.Middlewares;
// public class ExceptionHandlingMiddleware
// {
//     private readonly RequestDelegate _next;
//     private readonly ILogger<ExceptionHandlingMiddleware> _logger;
//
//     public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
//     {
//         _next = next;
//         _logger = logger;
//     }
//
//     public async Task Invoke(HttpContext context)
//     {
//         try
//         {
//             await _next(context);
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, "An error occurred");
//             await HandleExceptionAsync(context, ex);
//         }
//     }
//
//     private async Task HandleExceptionAsync(HttpContext context, Exception exception)
//     {
//         context.Response.ContentType = "application/json";
//         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//
//         var response = new ApiRespone<string>("An error occurred")
//         {
//             Errors = [exception.Message]
//         };
//
//         await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
//     }
// }
//

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Detail = exception.Message +" "+ exception.InnerException?.Message +" "+ exception.InnerException?.InnerException?.Message, 
            Instance = httpContext.Request.Path,
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
