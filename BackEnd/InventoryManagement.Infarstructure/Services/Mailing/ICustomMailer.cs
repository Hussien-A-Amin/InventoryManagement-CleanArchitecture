using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace InventoryManagement.Infarstructure.Services.Mailing;
public interface ICustomMailer
{
    Task SendEmailAsync(string email, string subject, string htmlMessage, List<string>? files = null);
}
