using InventoryManagement.Api;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();




builder
    .BuildBasic()
    .BuildExceptionHandler()
    .BuildDbContext()
    //.BuildIdentity()
    .BuildJwt()
    .BuildFluentValidation()
    .BuildMapster()

    ;

var h = builder.Services.Where(p => p.ServiceType == typeof(IAuthenticationSchemeProvider)).ToList();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
