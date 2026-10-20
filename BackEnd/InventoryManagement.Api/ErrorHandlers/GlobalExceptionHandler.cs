using EntityFramework.Exceptions.Common;
using InventoryManagement.Application.Dtos;
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
public enum ErrorType
{
    NotFound=14560,
    UniqueConstraint=14896,

}
public record Error(string Code, string Description)
{
    public static Error NotFound => new Error(ErrorType.NotFound.ToString(), "Entity you want not found");
}

public static class ResultExtention
{
    
    
}
public class Result
{
    [JsonPropertyOrder(1)]
    public bool IsSuccess { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(3)]
    public List<Error> Errors { get; set; } = default!;

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;

    }







    public static Result Success() => new (true);
    public static Result Failure() => new (false);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true);

 

    //public static Result<TValue> Failure<TValue>(Error error) => new(default, false);

    //internal static Result<T> Success<T>(Task<List<VotePerDayResponse>> votePerDay)
    //{
    //    throw new NotImplementedException();
    //}
}

public class Result<TValue>(TValue? value, bool isSuccess) : Result(isSuccess)
{
    private readonly TValue? _value = value;

    [JsonPropertyOrder(2)]
    public TValue? Data { get => value; } 
}
public static class ResultExtentions
{
    public static Result AddError(this Result result, Error error)
    {
        if (error != null)
        {
            if (result.Errors == null)
                result.Errors = [];

            result.Errors.Add(error);

        }
        return result;
    }
    public static ObjectResult ToProblem(this Result result, int statusCode)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Can not convert success result to a Problem");

        var problem = Results.Problem(statusCode: statusCode);
        var problemDetials = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetials!.Extensions = new Dictionary<string, object?>
        {
            {
              "errors" , result.Errors
            }
        };

        return new ObjectResult(problemDetials);
    }
    public static ObjectResult ToResult(this UserState state)
    {

        switch (state)
        {
             default:
                return Result.Failure()
                    .AddError(new Error("UserExist", "User already exist"))
                    .ToProblem(StatusCodes.Status404NotFound);
        }



    }
}





