using InventoryManagement.Api.Authentication.Jwt;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers.Auth;


[ApiController]
[Route("api/[controller]")]
public class AuthController(ApplicationDataUnit dataUnit, IAuthProvider authProvider, ApplicationUserManager userManager) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var userState = await userManager.RegisterAsync(request, cancellationToken);

        return userState.ToResult();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var usr = new ApplicationUser()
        {
            Id=Guid.CreateVersion7(),
            UserName = request.Email,
            Email = request.Password,
        };


        var authRes = authProvider.CreateToken(usr, new List<string>(), new List<string>());
        var rdd = authProvider.ValidateToken(authRes.Token);
        return authRes != null
            ? Ok(Result.Success(authRes))
            : Result.Failure().ToProblem(StatusCodes.Status401Unauthorized);
    }


    





    //[HttpPost]
    //[ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    //public async Task<IActionResult> Login(AuthRequest request, CancellationToken cancellationToken)
    //{
    //    var authRes = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

    //    return authRes.IsSuccess
    //        ? Ok(authRes.Value)
    //        : authRes.ToProblem(StatusCodes.Status401Unauthorized);
    //}

    //[HttpPost("RefreshToken")]
    //public async Task<IActionResult> RefreshToken(TokenRequest request, CancellationToken cancellationToken)
    //{
    //    var authRes = await _authService.GetRefreshTokenAsync(request.token, request.refreshToken, cancellationToken);

    //    return authRes.IsSuccess
    //       ? Ok(authRes.Value)
    //       : authRes.ToProblem(StatusCodes.Status400BadRequest);
    //}

    //[HttpPut("Revoke-Refresh-Token")]
    //public async Task<IActionResult> RevokeRefreshToken(TokenRequest request, CancellationToken cancellationToken)
    //{
    //    var isRevoked = await _authService.RevokeRefreshTokenAsync(request.token, request.refreshToken, cancellationToken);

    //    return isRevoked.IsSuccess
    //       ? Ok()
    //       : isRevoked.ToProblem(StatusCodes.Status400BadRequest);
    //}



    //[HttpPost("Email-Confirm")]
    //public async Task<IActionResult> EmailConfirm([FromBody] ConfirmEmailRequest request)
    //{
    //    var authRes = await _authService.ConfirmEmailAsync(request);

    //    return authRes.IsSuccess
    //        ? Ok()
    //        : authRes.ToProblem(StatusCodes.Status500InternalServerError);
    //}


    //[HttpPost("Resend-Confirmation-Email")]
    //public async Task<IActionResult> ResendConfirmationCode([FromBody] ResendConfirmationEmail request)
    //{
    //    var authRes = await _authService.ResendConfirmEmailAsync(request);

    //    return authRes.IsSuccess
    //        ? Ok()
    //        : Ok(authRes.Error);
    //}

    //[HttpPost("Forget-Password")]
    //public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    //{
    //    var authRes = await _authService.SendResetPasswordCodeAsync(request.Email);

    //    return authRes.IsSuccess
    //        ? Ok()
    //        : Ok(authRes.Error);
    //}

    //[HttpPost("Reset-Password")]
    //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    //{
    //    var authRes = await _authService.ResetPasswordAsync(request);
    //    return authRes.IsSuccess
    //        ? Ok()
    //        : Ok(authRes.Error);
    //}

    //[HttpGet("test")]
    //public IActionResult Test()
    //{
    //    Thread.Sleep(6000);
    //    return Ok("Test endpoint is working");
    //}
}
