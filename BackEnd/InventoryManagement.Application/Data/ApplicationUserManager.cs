using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Services.Mailing;
using InventoryManagement.Infarstructure.Costants;
using InventoryManagement.Infarstructure.EFCore;
using InventoryManagement.Infarstructure.Extentions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Application.Data;

public class ApplicationUserManager(UserManager<ApplicationUser> _userManager, ApplicationMailingService _mailingService, SignInManager<ApplicationUser> _signInManager, ILogger<ApplicationUserManager> _logger)
{
    public Dictionary<string, string> Errors = [];
    public ApplicationUser? CurrentUser;

    public async Task<UserState> CheckUser(AuthRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.UserNotFound;


        var IsVaild = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (IsVaild.Succeeded)
        {
            CurrentUser = user;

            var db = (ApplicationDbContext)_userManager.ServiceProvider.GetService(typeof(ApplicationDbContext))!;
           
            var refreshToken = "";

            var dbSet = db.Set<ApplicationSession>();

           var openCount=await dbSet.Where(p =>! p.IsCanceled&& p.UserId == CurrentUser.Id).CountAsync();
            if (openCount>5)
            {
                Errors["OpenedSessionsExceeded"]= "Opened Sessions Exceeded";
                return UserState.LoginFailed;
            }


            var session = new ApplicationSession
            {
                UserId = CurrentUser!.Id,
                RefreshToken = GenerateHashedRefreshToken(out refreshToken),
                Expires = DateTime.UtcNow.AddDays(14)

            };


            dbSet.Add(session);
            await db.SaveChangesAsync(cancellationToken);

            session.RawToken = refreshToken;
            CurrentUser.CurrentSession = session;

            //fetch roles and permissions

            var userRoles = db.UserRoles.Where(x => x.UserId == CurrentUser!.Id);



            CurrentUser.Roles =await db.Roles.RightJoin(userRoles, r => r.Id, ur => ur.RoleId, (r, ur) => r)
                .Select(x => x.Name!)
                .ToListAsync();



            CurrentUser.Permissions = await db.UserClaims
                .Where(x => x.UserId == CurrentUser!.Id)
                .ToDictionaryAsync(o=>o.ClaimType!, o=>o.ClaimValue);







            return UserState.LoginSucceeded;
        }

        if (IsVaild.IsLockedOut)
        {
            var sp = user.LockoutEnd?.Subtract(DateTimeOffset.UtcNow);



            Errors["UserLockedOut"] = $"You can try after {sp.Value.Normalize()} ";
            return UserState.UserLockedOut;
        }


        if (!user.EmailConfirmed)
            return UserState.EmailNotConfirmed;

        if (IsVaild.IsNotAllowed)
            return UserState.UserNotAllowed;
        else
            return UserState.LoginFailed;



    }
    public string GenerateHashedRefreshToken(out string refreshToken)
    {
        refreshToken = RandomNumberGenerator.GetHexString(32);

        var hashByte = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));

        return Convert.ToBase64String(hashByte);


    }
    
    public async Task<UserState> RefreshTokenAsync(string rawToken, CancellationToken cancellationToken = default)
    {
        var hashByte = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));

        var refreshToken= Convert.ToBase64String(hashByte);

        var db = (ApplicationDbContext)_userManager.ServiceProvider.GetService(typeof(ApplicationDbContext))!;

        var dbSet = db.Set<ApplicationSession>();

        var session = await dbSet.Include(p=>p.User).FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (session is null)
        {
            _logger.LogWarning("Refresh token not found for user ");
            return UserState.InvalidToken;
        }
        if (session.Expires < DateTime.UtcNow)
        {
            _logger.LogWarning("Refresh token expired for user {UserId}", session.UserId);
            return UserState.InvalidToken;
        }
        if (session.IsCanceled)
        {
            _logger.LogCritical("try to use canceled refresh token for user {UserId}", session.UserId);
            return UserState.InvalidToken;
        }

        session.RefreshToken = GenerateHashedRefreshToken(out var newRefreshToken);
        session.Expires = DateTime.UtcNow.AddDays(14);
        session.RawToken = newRefreshToken;
        await db.SaveChangesAsync(cancellationToken);
        CurrentUser = session.User;
        CurrentUser.CurrentSession = session;



        return UserState.LoginSucceeded;
    }

   



    public async Task<bool> Logout(Guid sessionId, CancellationToken cancellationToken)
    {

        var db = (ApplicationDbContext)_userManager.ServiceProvider.GetService(typeof(ApplicationDbContext))!;

        var dbSet = db.Set<ApplicationSession>();

        var session = await dbSet.Include(p => p.User).FirstOrDefaultAsync(x => x.Id == sessionId);
        if(session is null)
        {
            _logger.LogWarning("Session not found for sessionId {SessionId}", sessionId);
            return false;
        }
        
        session.IsCanceled = true;

        await db.SaveChangesAsync(cancellationToken);

        return true;


    }


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
            foreach (var item in result.Errors)
            {
                Errors[item.Code] = item.Description;
            }
            return UserState.RegistrationFailed;

        }

        //await _userManager.AddToRoleAsync(user, DefaultRole.Member);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        var callbackUrl = $"{ApplicationConstants.BaseUrl}/auth/ConfirmEmail?Email={user.Email}&code={code}";

        //_logger.LogInformation("Email Confirmation Link: {callbackUrl}", callbackUrl);

        await _mailingService.SendEmailConfirmationAsync(user.Email, callbackUrl);

        return UserState.RegisteredSuccessfully;
    }



    public async Task<UserState> SendResetPasswordCodeAsync(EmailObjectRequst request, CancellationToken cancellation)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.RestPasswordSent;


        if (!user.EmailConfirmed)
            return UserState.EmailNotConfirmed;


        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        //var callbackUrl = $"/auth/forget-password?userId={user.Id}&code={code}";

        //_logger.LogInformation("Email Reset Password Link: {callbackUrl}", callbackUrl);

        //await _mailingService.SendPasswordResetEmailAsync(user.Email, callbackUrl);


        //return Result.Success();
        return UserState.RestPasswordSent;
    }


    public async Task<UserState> ResendConfirmEmailAsync(EmailObjectRequst request, CancellationToken cancellation)
    {

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return UserState.EmailConfirmationSent;


        if (user.EmailConfirmed)
            return UserState.EmailAlreadyConfirmed;


        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));

        var callbackUrl = $"{ApplicationConstants.BaseUrl}/auth/ConfirmEmail?Email={user.Email}&code={code}";

        //_logger.LogInformation("Email Confirmation Link: {callbackUrl}", callbackUrl);

        await _mailingService.SendEmailConfirmationAsync(user.Email!, callbackUrl);
        return UserState.EmailConfirmationSent;
    }




    public async Task<UserState> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken)
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
        {
            foreach (var item in result.Errors)
            {
                Errors[item.Code] = item.Description;
            }

            return UserState.EmailConfirmationFailed;
        }

        return UserState.EmailConfirmedSuccessfully;



    }
    public async Task<UserState> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellation)
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
            foreach (var item in result.Errors)
            {
                Errors[item.Code] = item.Description;
            }
            return UserState.PasswordResetFailed;
        }
        catch (Exception)
        {
            return UserState.InvalidToken;
        }
    }

}
