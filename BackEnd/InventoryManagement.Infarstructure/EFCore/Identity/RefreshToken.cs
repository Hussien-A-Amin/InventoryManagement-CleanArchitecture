using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Infarstructure.EFCore;

public class ApplicationSession : BaseEntity
{
    public ApplicationUser User { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public bool IsCanceled { get; set; } = default!;
    public DateTime Expires { get; set; }
    public bool IsActive => !IsCanceled && DateTime.UtcNow < Expires;







    // This property is not mapped to the database and is used to hold the raw token for the session in memory.
    [NotMapped]
    public string? RawToken;
}
public sealed class ApplicationSessionConfiguration : IEntityTypeConfiguration<ApplicationSession>
{
    public void Configure(EntityTypeBuilder<ApplicationSession> b)
    {

        b.ToTable("ApplicationSessions");
        b
           .HasOne(x => x.User)
           .WithMany(x => x.Sessions)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Cascade);

    }
}
