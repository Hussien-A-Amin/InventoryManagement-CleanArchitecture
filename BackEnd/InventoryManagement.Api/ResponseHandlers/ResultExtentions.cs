using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.ResponseHandlers;

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
    public static Result AddMessages(this Result result, string message)
    {
        if (!string.IsNullOrEmpty( message ))
        {
                result.Message =message;


        }
        return result;
    }
    public static Result SetProplem(this Result result, string title, string detail)
    {
        if (!string.IsNullOrEmpty(title)&& !string.IsNullOrEmpty(detail))
        {
                result.Problem = new ProblemRecord(title, detail);


        }
        return result;
    }
    public static ObjectResult ToProblem(this Result result, int statusCode)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Can not convert success result to a Problem");

        var problem = Results.Problem(statusCode: statusCode);
        var problemDetials = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
        problemDetials.Title = result.Problem?.Title;
        problemDetials.Detail = result.Problem?.Detail;
        problemDetials!.Extensions = new Dictionary<string, object?>
        {
            {
              "errors" , result.Errors
            }
        };

        return new ObjectResult(problemDetials);
    }
    
}





