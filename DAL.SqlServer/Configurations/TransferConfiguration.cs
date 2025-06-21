using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("Transfers");


        builder.Property(t => t.FromDepoId).IsRequired();
        builder.Property(t => t.ToDepoId).IsRequired();
        builder.Property(t => t.ProductId).IsRequired();
        builder.Property(t => t.Quantity).IsRequired();
        builder.Property(t => t.TransferStatus)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(t => t.ApprovedByUserName)
               .HasMaxLength(100);

        builder.Property(t => t.ApprovedAt);

        builder.Property(t => t.CompanyId).IsRequired();
        builder.HasIndex(t => t.CompanyId);
        builder.HasIndex(t => t.FromDepoId);
        builder.HasIndex(t => t.ToDepoId);
        builder.HasIndex(t => t.ProductId);

        builder.HasOne(t => t.FromDepo)
               .WithMany()
               .HasForeignKey(t => t.FromDepoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToDepo)
               .WithMany()
               .HasForeignKey(t => t.ToDepoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Product)
               .WithMany()
               .HasForeignKey(t => t.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}