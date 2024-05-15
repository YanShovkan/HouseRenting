using HouseRenting.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Dal.EntityConfiguration;

internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.StartDate).IsRequired();
        builder.Property(t => t.EndDate).IsRequired();
        builder.Property(t => t.CommitedDate).IsRequired(false);
        builder.Property(t => t.IsCommited).IsRequired().HasDefaultValue(false);
        builder.HasOne(t => t.Admin).WithMany(a => a.Orders).HasForeignKey(t => t.AdminId);
        builder.HasOne(t => t.Advert).WithMany(a => a.Orders).HasForeignKey(t => t.AdvertId);
    }
}