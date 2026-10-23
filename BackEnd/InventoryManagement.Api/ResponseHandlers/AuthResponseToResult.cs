using InventoryManagement.Application.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.ResponseHandlers;

public static class AuthResponseToResult
{

    public static ObjectResult ToResult(this UserState state, Dictionary<string, string>? errors = null)
    {

        switch (state)
        {

            case UserState.RegisteredSuccessfully:
                return new OkObjectResult(Result.Success()
                    .AddMessages("we sent email confirmation for you , check your email box and use code for confirmation"));

            case UserState.RestPasswordSent:
                return new OkObjectResult(Result.Success()
                    .AddMessages("we sent Password reset code for you , check your email box and use code for password reset"));
            
            case UserState.PasswordResetSuccessfully:
                return new OkObjectResult(Result.Success()
                    .AddMessages("Password Reset Successfully"));
            
            case UserState.EmailAlreadyConfirmed:
                return new OkObjectResult(Result.Success()
                    .AddMessages("Email Already Confirmed"));

            case UserState.EmailConfirmationSent:
                return new OkObjectResult(Result.Success()
                    .AddMessages("we sent email confirmation for you , check your email box and use code for confirmation"));

            case UserState.EmailConfirmedSuccessfully:
                return new OkObjectResult(Result.Success()
                    .AddMessages("Email Confirmed Successfully"));







            case UserState.RegistrationFailed:
                return Result.Failure()
                    .MapErrors(errors)
                    .ToProblem(StatusCodes.Status409Conflict);

            case UserState.EmailConfirmationFailed:
                return Result.Failure()
                    .MapErrors(errors)
                    .ToProblem(StatusCodes.Status409Conflict);

            case UserState.PasswordResetFailed:
                return Result.Failure()
                    .MapErrors(errors)
                    .ToProblem(StatusCodes.Status409Conflict);



            case UserState.InvalidToken:
                return Result.Failure()
                    .AddError(new Error("InvalidToken", "Invalid Token"))
                    .ToProblem(StatusCodes.Status400BadRequest);





            case UserState.LoginFailed:
                return Result.Failure()
                    .AddError(new Error("LoginFailed", "Login Failed"))
                    .MapErrors(errors)
                    .ToProblem(StatusCodes.Status400BadRequest);
            case UserState.EmailNotConfirmed:
                return Result.Failure()
                    .AddError(new Error("EmailNotConfirmed", "Confirm Email First"))
                    .ToProblem(StatusCodes.Status400BadRequest);






            case UserState.UserNotFound:
                return Result.Failure()
                    .AddError(new Error("UserNotFound", "User Not Found"))
                    .ToProblem(StatusCodes.Status404NotFound);
          
            
           



            case UserState.UserExist:
                return Result.Failure()
                    .AddError(new Error("UserExist", "User already exist"))
                    .ToProblem(StatusCodes.Status409Conflict);
           
            
            case UserState.UserNotAllowed:
                return Result.Failure()
                    .AddError(new Error("UserNotAllowed", "User Not Allowed"))
                    .ToProblem(StatusCodes.Status400BadRequest);


            case UserState.UserLockedOut:
                return Result.Failure()
                    .MapErrors(errors)
                    .ToProblem(StatusCodes.Status400BadRequest);








            default:
                return Result.Failure()
                    .AddError(new Error("UserExist", "User already exist"))
                    .ToProblem(StatusCodes.Status404NotFound);
        }
    }
    public static Result MapErrors(this Result result, Dictionary<string, string>? errors)
    {
        if (errors != null && errors.Any())
        {
            foreach (var error in errors)
            {
                result.AddError(new Error(error.Key, error.Value));
            }
        }

        return result;

    }
}





