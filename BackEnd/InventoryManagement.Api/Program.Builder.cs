using EntityFramework.Exceptions.SqlServer;
using FluentValidation;
using FluentValidation.AspNetCore;
using InventoryManagement.Api.Authentication.Jwt;
using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Services.Mailing;
using InventoryManagement.Infarstructure.EFCore;
using InventoryManagement.Infarstructure.Services.Mailing;
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
    public static WebApplicationBuilder BuildIdentity(
     this WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(
            options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });

        return builder;
    }
    public static WebApplicationBuilder BuildJwt(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration
            .GetSection("Jwt").Get<JwtOptions>();

       

        var key = Encoding.UTF8.GetBytes(jwtSettings!.Key);

        builder.Services.AddSingleton(jwtSettings);
        builder.Services.AddScoped<IAuthProvider, JwtProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(key),

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
               
            });

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





    public static WebApplicationBuilder BuildUserManager(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ApplicationUserManager>();
        return builder;
    }





    public static WebApplicationBuilder BuildMailing(this WebApplicationBuilder builder)
    {


        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));



        builder.Services.AddSingleton<ICustomMailer, MemeMailer>();
        builder.Services.AddSingleton<ApplicationMailingService>();

        return builder;
    }





    public static WebApplicationBuilder Build(this WebApplicationBuilder builder)
    {

        return builder;
    }

}
