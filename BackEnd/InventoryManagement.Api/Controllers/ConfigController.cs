using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetConfig()
    {
        // Read a standard configuration value (which may be overridden by an environment variable)
        string customSetting = _configuration["MyCustomSetting"];
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        string testConnection = _configuration["testconn"];

        // Read a connection string
        //string connectionString = _configuration.GetConnectionString("DefaultConnection");

        // Read directly from Environment variables if needed
        string rawEnvVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return Ok(new
        {
            CustomSetting = customSetting,
            //ConnectionString = connectionString,
            Environment = rawEnvVar,
            ConnectionString= connectionString,
            TestConnection = testConnection
        });
    }
}