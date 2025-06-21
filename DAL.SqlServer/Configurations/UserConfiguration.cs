using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(u => u.UserName)
               .IsUnique();

        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(u => u.Role)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(u => u.CompanyId)
               .IsRequired();

        builder.HasIndex(u => u.CompanyId);
    }
}
