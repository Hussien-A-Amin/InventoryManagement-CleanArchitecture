using InventoryManagement.Application.Dtos;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Application.Data;

public class ApplicationUserManager(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
{


    public async Task<UserState> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var isEmailExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isEmailExist)
            return UserState.UserExist;

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return UserState.RegistrationFailed;


        }
        return UserState.RegisteredSuccessfully;

        //await _userManager.AddToRoleAsync(user, DefaultRole.Member);

        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        //var callbackUrl = $"auth/confirm-email?userId={user.Id}&code={code}";

        //_logger.LogInformation("Email Confirmation Link: {callbackUrl}", callbackUrl);

        //await SendConfirmationEmailAsync(user, callbackUrl);

        //return Result.Success();
    }



    public async Task<UserState> SendResetPasswordCodeAsync(EmailObjectRequst request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.RestPasswordSent;


        if (!user.EmailConfirmed)
            return UserState.EmailNotConfirmed;


        return UserState.RestPasswordSent;
        //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        //var callbackUrl = $"/auth/forget-password?userId={user.Id}&code={code}";

        //_logger.LogInformation("Email Reset Password Link: {callbackUrl}", callbackUrl);

        //await SendResetPasswordAsync(user, callbackUrl);

        //return Result.Success();
    }


    public async Task<UserState> ResendConfirmEmailAsync(EmailObjectRequst request)
    {

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.EmailConfirmationSent;


        if (user.EmailConfirmed)
            return UserState.EmailAlreadyConfirmed;

        return UserState.EmailConfirmationSent;

        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        //var callbackUrl = $"/auth/confirm-email?userId={user.Id}&code={code}";

        //_logger.LogInformation("Email Confirmation Link: {callbackUrl}", callbackUrl);

        //await SendConfirmationEmailAsync(user, callbackUrl);

        //return Result.Success();
    }


    //private async Task SendConfirmationEmailAsync(ApplicationUser user, string confirmationLink)
    //{
    //    var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

    //    var body = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
    //        new Dictionary<string, string>
    //        {
    //            { "{name}" , $"{user.FirstName} {user.LastName}" },
    //            { "{verficationCode}" , $"{origin}/{confirmationLink}" },
    //        }
    //    );

    //    BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Survey Basket : Confirmation Email", body));

    //    await Task.CompletedTask;
    //}


    public async Task<UserState> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.UserNotFound;


        if (user.EmailConfirmed)
            return UserState.EmailAlreadyConfirmed;



        var code = request.Code;

        try
        {
            code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
        }
        catch (Exception)
        {
            return UserState.InvalidToken;

        }

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
            return UserState.EmailConfirmationFailed;

        return UserState.EmailConfirmedSuccessfully;



    }
    public async Task<UserState> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.email);

        if (user is null)
            return UserState.UserNotFound;

        if (!user.EmailConfirmed)
            return UserState.EmailNotConfirmed;


        try
        {
            var code = Encoding.UTF8.GetString(Convert.FromBase64String(request.code));
            var result = await _userManager.ResetPasswordAsync(user, code, request.newPassword);
            if (result.Succeeded)
                return UserState.PasswordResetSuccessfully;
            return UserState.PasswordResetFailed;
        }
        catch (Exception)
        {
            return UserState.InvalidToken;
        }
    }

}
