using HouseRenting.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Dal.EntityConfiguration;

public class AdminEntityConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("admins");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.Login).IsRequired();
        builder.Property(t => t.Password).IsRequired();
        builder.Property(t => t.Email).IsRequired();
        builder.HasMany(t => t.Orders).WithOne(o => o.Admin);
    }
}
