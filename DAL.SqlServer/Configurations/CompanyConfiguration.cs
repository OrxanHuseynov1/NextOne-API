using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace DAL.SqlServer.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.PackageEndDate)
            .IsRequired();

        builder.Property(x => x.AutoSubtractStock)
            .IsRequired();

        builder.Property(x => x.SaleType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.IsReceptionUpdate)
            .IsRequired();

        builder.Property(x => x.ReceiptType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.InstagramLink)
            .HasMaxLength(255);

        builder.Property(x => x.TikTokLink)
            .HasMaxLength(255);

        builder.Property(x => x.ShowAddressOnReceipt)
            .IsRequired();
    }
}