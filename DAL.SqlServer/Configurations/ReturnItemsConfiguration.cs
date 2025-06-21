using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class ReturnItemConfiguration : IEntityTypeConfiguration<ReturnItem>
{
    public void Configure(EntityTypeBuilder<ReturnItem> builder)
    {
        builder.ToTable("ReturnItems");

        builder.Property(ri => ri.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ri => ri.Quantity)
            .IsRequired();

        builder.Property(ri => ri.OrderIndex)
            .IsRequired();

        builder.Property(ri => ri.ProductId)
            .IsRequired();

        builder.Property(ri => ri.ReturnId)
            .IsRequired();

        builder.Property(ri => ri.CompanyId)
            .IsRequired();

        builder.HasIndex(ri => ri.CompanyId);
        builder.HasIndex(ri => ri.ProductId);
        builder.HasIndex(ri => ri.ReturnId);

        builder.HasOne(ri => ri.Product)
            .WithMany()
            .HasForeignKey(ri => ri.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ri => ri.Return)
            .WithMany(r => r.Items)
            .HasForeignKey(ri => ri.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
