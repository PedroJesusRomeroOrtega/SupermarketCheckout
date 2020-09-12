﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarkerCheckout.Core.Entities;

namespace SupermarkerCheckout.Infrastructure.Data.Config
{
    public class SkuConfiguration : IEntityTypeConfiguration<Sku>
    {
        public void Configure(EntityTypeBuilder<Sku> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Sku.SkuPrices));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(s => s.Name)
                .IsUnique();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
