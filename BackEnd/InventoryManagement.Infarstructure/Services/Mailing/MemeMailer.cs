using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InventoryManagement.Infarstructure.Services.Mailing;

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; } = 587;
    public string Username { get; set; }
    public string AppPassword { get; set; }
}

public class MemeMailer(IOptions<SmtpSettings> _smtpOptions) : ICustomMailer
{
    SmtpSettings SmtpSettings = _smtpOptions.Value;
    // Best practice: Inject configuration or a settings class here
    // private readonly EmailSettings _settings;

    //public async Task SendEmailAsync(string email, string subject, string htmlMessage, List<IFormFile>? files = null)
    //{
    //    var message = new MimeMessage();

    //    // 1. Map to your parameters/dynamic data
    //    message.From.Add(new MailboxAddress("Esnawy", "Admin@Esnawy.com"));
    //    message.To.Add(MailboxAddress.Parse(email)); // Uses the parameter passed into the method
    //    message.Subject = subject;

    //    var builder = new BodyBuilder();


    //    builder.HtmlBody = htmlMessage;
    //    builder.TextBody = "This email requires an HTML compatible email viewer."; // Plain text fallback







    //    // Complete the body compilation
    //    message.Body = builder.ToMessageBody();

    //    // 4. Send using secure, environment-based credentials
    //    using (var client = new MailKit.Net.Smtp.SmtpClient())
    //    {
    //        // Note: In production, pull these from Web.config or appsettings.json
    //        string smtpServer = "smtp.gmail.com";
    //        int port = 587;
    //        string username = "Hussien.a.amin@gmail.com";
    //        string appPassword = "ahfm httg hmue ooop"; // Pro tip: Rotate this password now since it was exposed!

    //        await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
    //        await client.AuthenticateAsync(username, appPassword);
    //        await client.SendAsync(message);
    //        await client.DisconnectAsync(true);
    //    }
    //}
    public async Task SendEmailAsync(string email, string subject, string htmlMessage, List<string>? files = null)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("Esnawy", "Admin@Esnawy.com"));
        message.To.Add(MailboxAddress.Parse(email)); 
        message.Subject = subject;

        var builder = new BodyBuilder();


        builder.HtmlBody = htmlMessage;
        builder.TextBody = "This email requires an HTML compatible email viewer."; 

        files?.ForEach(filePath =>
        {
            if (File.Exists(filePath))
            {
                builder.Attachments.Add(filePath);
            }
        });





        message.Body = builder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            string smtpServer = SmtpSettings.Host;
            int port = SmtpSettings.Port;
            string username = SmtpSettings.Username;
            string appPassword = SmtpSettings.AppPassword; 

            await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(username, appPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }



}
