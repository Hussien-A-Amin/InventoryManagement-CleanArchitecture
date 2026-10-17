using InventoryManagement.Application.Data;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Api;

public static class ProgramExtention
{
    public static WebApplicationBuilder BuildDbContext(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
            o => o
            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            ));



        builder.Services.AddScoped<ApplicationDataUnit>();
        return builder;
    }
}
