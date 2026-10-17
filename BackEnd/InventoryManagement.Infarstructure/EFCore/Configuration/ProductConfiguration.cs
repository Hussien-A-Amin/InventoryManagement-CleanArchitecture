using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Infarstructure.EFCore.Configuration;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> b)
    {
        b.ToTable("Shop_Products");
        b.HasIndex(x => x.Title).IsUnique();

        b.Property(x => x.Title).HasMaxLength(250).IsRequired();


        b.Property(x => x.Price).HasPrecision(18, 2).HasMaxLength(18);

    }
}
