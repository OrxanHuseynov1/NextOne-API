using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);

        builder.Property(si => si.BoxCount)
            .IsRequired();

        builder.Property(si => si.Count)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(si => si.OrderIndex)
            .IsRequired();

        builder.Property(si => si.SaleId)
            .IsRequired();

        builder.Property(si => si.ProductId)
            .IsRequired();

        builder.Property(si => si.CompanyId)
            .IsRequired();

        builder.HasIndex(si => si.CompanyId);
        builder.HasIndex(si => si.SaleId);
        builder.HasIndex(si => si.ProductId);

        // Audit sahələri
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.LastModifiedAt);
        builder.Property(x => x.LastModifiedBy);
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(si => si.Product)
            .WithMany()
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
