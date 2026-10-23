using InventoryManagement.Infarstructure.Services.Mailing;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Services.Mailing;

public class ApplicationMailingService(ICustomMailer mailer)
{
    public async Task SendEmailConfirmationAsync(string email, string returnUrl)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "EmailConfirmationTemplate.html");
        string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

        //// 2. Replace the tokens dynamically
        htmlContent = htmlContent
            .Replace("{returnUrl}", returnUrl)
           ;


        await mailer.SendEmailAsync(email, "تأكيد البريد الإلكتروني", htmlContent);




    }
    public async Task SendPasswordResetEmailAsync(string email, string returnUrl)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "EmailPasswordResetConfirmation.html");
        string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

        //// 2. Replace the tokens dynamically
        htmlContent = htmlContent
            .Replace("{returnUrl}", returnUrl)
           ;


        await mailer.SendEmailAsync(email, "تأكيد إعادة تعيين كلمة المرور", htmlContent);




    }
    public async Task SendAdminInvitationEmailAsync(string email, string returnUrl)
    {
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "EmailAdminInvaitationTemplate.html");
        string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);

        //// 2. Replace the tokens dynamically
        htmlContent = htmlContent
            .Replace("{returnUrl}", returnUrl)
           ;


        await mailer.SendEmailAsync(email, "دعوة للانضمام كمسؤول", htmlContent);




    }
   


}
