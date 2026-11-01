
using InventoryManagement.Application.Services.Mailing;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace InventoryManagement.Api.HealthChecks;

public class PasswordCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var http = httpClientFactory.CreateClient();

        var response = await http.GetAsync("https://localhost:7034/api/Products", cancellationToken);


        if (response.IsSuccessStatusCode||response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
        {
            
            return HealthCheckResult.Healthy();

        }
        return HealthCheckResult.Unhealthy("un");
    }
}
public class DataBaseCheck(ApplicationDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var conn =await dbContext.Database.CanConnectAsync();

        if (conn)
        {
            
            return HealthCheckResult.Healthy();

        }
        return HealthCheckResult.Unhealthy();
    }
}
public class MailServiceHealthCheck(ApplicationMailingService mailingService) : IHealthCheck
{

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
          await  mailingService.SendEmailConfirmationAsync("hussien.a.amin@gmail.com", "https://localhost:7034/health");
            return await Task.FromResult(HealthCheckResult.Healthy());  
        }
        catch (Exception exp)
        {
            return await Task.FromResult(HealthCheckResult.Unhealthy("Mail service is unhealthy", exp));
        }
    }
}
