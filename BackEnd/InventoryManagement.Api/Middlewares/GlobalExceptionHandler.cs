using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Api.ErrorHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Something went wrong: {Message}", exception.Message);

        var problemDetials = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,

            Detail = exception.Message,
            Title = "Internal Server Error"
        };


        switch (exception)
        {
            case InvalidOperationException:

                problemDetials.Title = "InvalidOperationException";
                problemDetials.Detail = exception.Message;
                break;

            case UniqueConstraintException:
                problemDetials.Title = "UniqueConstraintException";
                problemDetials.Detail = exception.Message;
                break;

            case ReferenceConstraintException:
                problemDetials.Title = "ReferenceConstraintException";
                problemDetials.Detail = exception.Message;
                break;

            case DbUpdateException:
                problemDetials.Title = "DbUpdateException";
                problemDetials.Detail = exception.Message;
                break;

            case HttpRequestException:
                problemDetials.Title = "HttpRequestException";
                problemDetials.Detail = exception.Message;
                break;
            case KeyNotFoundException:
                problemDetials.Title = "KeyNotFoundException";
                problemDetials.Detail = exception.Message;
                break;


        }










        //KafErrorResponse response = new KafErrorResponse();




        //context.Response.StatusCode = StatusCodes.Status409Conflict;
        //if (ex is KafCustomException exception)
        //{
        //    response.Code = exception.Type;
        //}
        //else if (ex is KafManagerException exManage)
        //{
        //    response.Message = exManage.Message;
        //}
        //else
        //{

        //}










        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetials, cancellationToken: cancellationToken);
        return true;
    }
}





