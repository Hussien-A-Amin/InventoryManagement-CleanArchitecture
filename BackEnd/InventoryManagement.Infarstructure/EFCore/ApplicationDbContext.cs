using InventoryManagement.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;

    //add-migration Init-Create - Project InventoryManagement.Infarstructure -StartupProject InventoryManagement.Api -Context ApplicationDbContext
namespace InventoryManagement.Infarstructure.EFCore;
public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries())
        {

            if (entry.Entity is AuditableEntity audit)
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        audit.CreatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }



    public override int SaveChanges()
    {
        throw new Exception("Sync SaveChanges Not Allowed");
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //modelBuilder
        //    .ConfigPortalModel();



        modelBuilder.UseCollation("Arabic_CI_AI");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);



        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = new Guid("019f4ece-81d1-789a-b28a-66ab095e780a"),
                UserName = "Console",
                NormalizedUserName = "CONSOLE",
                Email = "hussien.a.amin@gmail.com",
                NormalizedEmail = "HUSSIEN.A.AMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEO4fD5HhuV2NhwJVHAuHcYLlUCRRoK7jMSAzDseVNWbwO3QV5rOtt/nqtGMt6+nMig==",
                SecurityStamp = "019f4ecf-b175-70d8-88d4-7f75920e5e4a",
                ConcurrencyStamp = "6164dd67-48d4-4458-8a04-b3b05cc9d1ed",


            });
        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = new Guid("019f4ece-81d3-71f1-9cb2-36b81367ca58"),
                Name = "Console",
                NormalizedName = "CONSOLE",
                ConcurrencyStamp = "b3e72ba3-66ea-49a0-87fc-332a61f08f2f",

            });
        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = new Guid("019f4efc-c706-75f8-abd6-b9ad8c185271"),
                Name = "PortalAdmin",
                NormalizedName = "PORTALADMIN",
                ConcurrencyStamp = "019f4efc-c707-70c5-a706-1945a19af8d2",
            });



        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = new Guid("019f4ece-81d1-789a-b28a-66ab095e780a"),
                RoleId = new Guid("019f4ece-81d3-71f1-9cb2-36b81367ca58"),

            });
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = new Guid("019f4ece-81d1-789a-b28a-66ab095e780a"),
                RoleId = new Guid("019f4efc-c706-75f8-abd6-b9ad8c185271"),

            });



    }
}
