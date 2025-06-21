using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(c => c.BuyType)
            .HasConversion<int>() 
            .IsRequired();

        builder.Property(c => c.DebtAmount)
            .HasColumnType("decimal(18,2)");

        builder.HasMany(c => c.Sales)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.DebtRecords)
            .WithOne(d => d.Customer)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.CompanyId)
            .IsRequired();

        builder.HasIndex(c => c.CompanyId);
    }
}
