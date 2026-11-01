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
    .BuildIdentity()
    .BuildJwt()
    .BuildFluentValidation()
    .BuildMapster()
    .BuildUserManager()
    .BuildMailing()

    ;



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();