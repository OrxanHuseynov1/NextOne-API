using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("Returns");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.PaymentType)
            .IsRequired();

        builder.Property(r => r.DebtReduction)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.CustomerId);

        builder.Property(r => r.CompanyId)
            .IsRequired();

        builder.HasIndex(r => r.CompanyId);
        builder.HasIndex(r => r.CustomerId);

        // Audit fields
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.LastModifiedAt);
        builder.Property(x => x.LastModifiedBy);
        builder.Property(x => x.DeletedAt);
        builder.Property(x => x.DeletedBy);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(r => r.Items)
            .WithOne(ri => ri.Return)
            .HasForeignKey(ri => ri.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}