using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class ProductInDepoConfiguration : IEntityTypeConfiguration<ProductInDepo>
{
    public void Configure(EntityTypeBuilder<ProductInDepo> builder)
    {
        builder.ToTable("ProductInDepos");

        builder.Property(pid => pid.Quantity)
            .IsRequired();

        builder.Property(pid => pid.ProductId)
            .IsRequired();

        builder.Property(pid => pid.DepoId)
            .IsRequired();

        builder.Property(pid => pid.CompanyId)
            .IsRequired();

        builder.HasIndex(pid => pid.CompanyId);
        builder.HasIndex(pid => pid.ProductId);
        builder.HasIndex(pid => pid.DepoId);

        builder.HasOne(pid => pid.Product)
            .WithMany(p => p.ProductInDepos)
            .HasForeignKey(pid => pid.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pid => pid.Depo)
            .WithMany(d => d.ProductInDepos)
            .HasForeignKey(pid => pid.DepoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
