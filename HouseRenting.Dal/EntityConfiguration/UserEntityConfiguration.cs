using HouseRenting.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRenting.Dal.EntityConfiguration;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.Login).IsRequired();
        builder.Property(t => t.Password).IsRequired();
        builder.Property(t => t.Email).IsRequired();
        builder.HasMany(t => t.Requests).WithOne(o => o.User);
        builder.HasMany(t => t.Adverts).WithOne(o => o.User);
    }
}
