using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoggerController(ILogger<LoggerController> logger) : ControllerBase
{
    [HttpPost("log-info-simple")]
    public IActionResult LogInfo(string message)
    {
        var start1=Stopwatch.StartNew();

        
            logger.LogInformation($"{message}");

        
        start1.Stop();
        logger.LogCritical("elapsed time start1: {elapsed}", start1.ElapsedMilliseconds);


        return Ok();
    }
    [HttpPost("log-info-template")]
    public IActionResult Logtemplate(string message)
    {
        logger.LogInformation(message+"     {date:}",DateTime.Now);
        return Ok();
    }

}