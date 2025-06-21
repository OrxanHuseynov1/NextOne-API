using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class DepoConfiguration : IEntityTypeConfiguration<Depo>
{
    public void Configure(EntityTypeBuilder<Depo> builder)
    {
        builder.ToTable("Depos");


        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.CompanyId)
            .IsRequired();

        builder.HasIndex(d => d.CompanyId); 

        builder.HasMany(d => d.ProductInDepos)
            .WithOne(pid => pid.Depo)
            .HasForeignKey(pid => pid.DepoId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
