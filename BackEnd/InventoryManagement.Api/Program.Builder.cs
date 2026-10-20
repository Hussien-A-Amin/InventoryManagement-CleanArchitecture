using EntityFramework.Exceptions.SqlServer;
using FluentValidation;
using FluentValidation.AspNetCore;
using InventoryManagement.Api.Authentication.Jwt;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Infarstructure.EFCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace InventoryManagement.Api;

public static class ProgramExtention
{
    public static WebApplicationBuilder BuildBasic(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        return builder;
    }
    public static WebApplicationBuilder BuildExceptionHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        return builder;
    }

    public static WebApplicationBuilder BuildDbContext(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options
            .UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                o => o
                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                )
            .UseExceptionProcessor()

            );

        builder.Services.AddScoped<ApplicationDataUnit>();
        return builder;
    }
    public static WebApplicationBuilder BuildIdentity(this WebApplicationBuilder builder)
    {

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

       
        return builder;
    }
    public static WebApplicationBuilder BuildJwt(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("jwt").Get<JwtOptions>();

        builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,o =>
        {
            o.SaveToken = true;

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                ClockSkew = TimeSpan.Zero,

            };
        });

        builder.Services.AddSingleton(jwtSettings!);
        builder.Services.AddSingleton<IAuthProvider, JwtProvider>();
     
        return builder;
    }





    public static WebApplicationBuilder BuildFluentValidation(this WebApplicationBuilder builder)
    {
       
        builder.Services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Application.Filters.PublicFilterViewModel)));

        return builder;

    }









    public static WebApplicationBuilder BuildMapster(this WebApplicationBuilder builder)
    {
        var MappingConfig = TypeAdapterConfig.GlobalSettings;
        MappingConfig.Scan(Assembly.GetAssembly(typeof(Application.Filters.PublicFilterViewModel))!);
        
        builder.Services.AddSingleton<IMapper>(new Mapper(MappingConfig));

        builder.Services.AddMapster();
        return builder;
    }
    public static WebApplicationBuilder Build(this WebApplicationBuilder builder)
    {

        return builder;
    }

}
