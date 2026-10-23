using InventoryManagement.Api.Authentication.Jwt;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers.Auth;


[ApiController]
[Route("api/[controller]")]
public class AuthController(ApplicationDataUnit dataUnit, IAuthProvider _authProvider, ApplicationUserManager userManager) : ControllerBase
{

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AuthRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.CheckUser(request, cancellationToken);
        if (userState != UserState.LoginSucceeded)
        {
            return userState.ToResult(userManager.Errors);
        }
        var authRes = _authProvider.CreateToken(userManager.CurrentUser!);
        
        return authRes != null
            ? Ok(Result.Success(authRes))
            : Result.Failure().ToProblem(StatusCodes.Status401Unauthorized);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody]TokenRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.RefreshTokenAsync( request.RefreshToken, cancellationToken);

        if (userState != UserState.LoginSucceeded)
        {
            return userState.ToResult(userManager.Errors);
        }
        var authRes = _authProvider.CreateToken(userManager.CurrentUser!);
        
        return authRes != null
           ? Ok(Result.Success(authRes))
           : Result.Failure().ToProblem(StatusCodes.Status401Unauthorized);
    }



    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var sessionId= User.FindFirst("SessionId")?.Value;
        Guid sessionGuid= Guid.Empty;
        if (sessionId == null||!Guid.TryParse(sessionId, out sessionGuid))
        {
            return Result.Failure().ToProblem(StatusCodes.Status401Unauthorized);
        }

        

        var result = await userManager.Logout(sessionGuid, cancellationToken);
        if (!result)
        {
            return Result.Failure().ToProblem(StatusCodes.Status401Unauthorized);
        }
        return Ok(Result.Success());
    }




    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.RegisterAsync(request, cancellationToken);

        return userState.ToResult(userManager.Errors);
    }




    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.ConfirmEmailAsync(request, cancellationToken);

        return userState.ToResult(userManager.Errors);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.ResetPasswordAsync(request, cancellationToken);

        return userState.ToResult(userManager.Errors);
    }


    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> SendResetPasswordCode([FromBody] EmailObjectRequst request, CancellationToken cancellationToken)
    {
        var userState = await userManager.SendResetPasswordCodeAsync(request, cancellationToken);

        return userState.ToResult(userManager.Errors);
    }


    [HttpPost("ResendConfirmEmail")]
    public async Task<IActionResult> ResendConfirmEmail([FromBody] EmailObjectRequst request, CancellationToken cancellationToken)
    {
        var userState = await userManager.ResendConfirmEmailAsync(request, cancellationToken);

        return userState.ToResult(userManager.Errors);
    }







   



}
