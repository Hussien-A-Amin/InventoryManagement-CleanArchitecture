using InventoryManagement.Api;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Api.HealthChecks;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddOpenApi();



builder
    .BuildBasic()
    .BuildLoging()
    .BuildOpemTelemetry()
    .BuildExceptionHandler()
    .BuildDbContext()
    .BuildIdentity()
    .BuildJwt()
    .BuildFluentValidation()
    .BuildMapster()
    .BuildUserManager()
    .BuildMailing()
    .BuildHealthCheck()
    .BuildCors()

    ;






var app = builder.Build();
//app.UseRequestResponseLogging();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();