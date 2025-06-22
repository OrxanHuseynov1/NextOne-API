using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("Returns");

        builder.Property(r => r.PaymentType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.DebtReduction)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.CustomerId);

        builder.Property(r => r.CompanyId)
            .IsRequired();

        builder.HasIndex(r => r.CompanyId);
        builder.HasIndex(r => r.CustomerId);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Returns)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Company)
            .WithMany()
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}