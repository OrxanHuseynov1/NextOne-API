using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.Property(s => s.PaymentType)
            .HasConversion<int>()    
            .IsRequired();

        builder.Property(s => s.TotalDiscount)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.DebtLeft)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.SaleStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(s => s.CustomerId);

        builder.Property(s => s.CompanyId)
            .IsRequired();

        builder.HasIndex(s => s.CompanyId);
        builder.HasIndex(s => s.CustomerId);


        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.SaleItems)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
