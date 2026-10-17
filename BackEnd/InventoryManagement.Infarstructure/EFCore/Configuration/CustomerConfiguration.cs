using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infarstructure.EFCore.Configuration;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> b)
    {
        b.ToTable("Shop_Customers");

        b.HasIndex(x => x.Name).IsUnique();
        b.Property(x => x.Name).HasMaxLength(250).IsRequired();

        b.Property(x => x.CreditLimit).HasPrecision(18, 2).HasMaxLength(18);
        b.Property(x => x.Credit).HasPrecision(18, 2).HasMaxLength(18);

    }
}
