using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Barcode)
            .HasMaxLength(100);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.PurchasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.WholesalePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.RetailPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.CompanyId)
            .IsRequired();

        builder.HasIndex(p => p.CompanyId);

        builder.Property(p => p.CategoryId);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.ProductInDepos)
            .WithOne(pid => pid.Product)
            .HasForeignKey(pid => pid.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
