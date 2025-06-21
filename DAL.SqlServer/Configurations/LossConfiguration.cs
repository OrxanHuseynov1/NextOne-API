using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class LossConfiguration : IEntityTypeConfiguration<Loss>
{
    public void Configure(EntityTypeBuilder<Loss> builder)
    {
        builder.ToTable("Losses");

        builder.Property(l => l.Quantity)
            .IsRequired();

        builder.Property(l => l.Reason)
            .HasMaxLength(500);

        builder.Property(l => l.ProductId)
            .IsRequired();

        builder.Property(l => l.DepoId)
            .IsRequired();

        builder.Property(l => l.CompanyId)
            .IsRequired();

        builder.HasIndex(l => l.CompanyId);
        builder.HasIndex(l => l.ProductId);
        builder.HasIndex(l => l.DepoId);


        builder.HasOne(l => l.Product)
            .WithMany()
            .HasForeignKey(l => l.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Depo)
            .WithMany()
            .HasForeignKey(l => l.DepoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
