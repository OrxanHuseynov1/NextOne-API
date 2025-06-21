using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class DebtRecordConfiguration : IEntityTypeConfiguration<DebtRecord>
{
    public void Configure(EntityTypeBuilder<DebtRecord> builder)
    {
        builder.ToTable("DebtRecords");

        builder.Property(d => d.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(d => d.Description)
            .HasMaxLength(100);

        builder.Property(d => d.Date)
            .IsRequired();

        builder.Property(d => d.CustomerId)
            .IsRequired();

        builder.Property(d => d.CompanyId)
            .IsRequired();

        builder.HasIndex(d => d.CompanyId); 
        builder.HasIndex(d => d.CustomerId); 

        builder.HasOne(d => d.Customer)
            .WithMany(c => c.DebtRecords)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
