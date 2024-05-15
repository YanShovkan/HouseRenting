using HouseRenting.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HouseRenting.Common.Enums;

namespace HouseRenting.Dal.EntityConfiguration;

public class RequestEntityConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("requests");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.PriceLess).IsRequired(false);
        builder.Property(t => t.PriceGreater).IsRequired(false);
        builder.Property(t => t.AreaLess).IsRequired(false);
        builder.Property(t => t.AreaGreater).IsRequired(false);
        builder.Property(t => t.RoomsCountLess).IsRequired(false);
        builder.Property(t => t.RoomsCountGreater).IsRequired(false);
        builder.Property(t => t.MonthCount).IsRequired();
        builder.Property(t => t.Status).IsRequired().HasDefaultValue(RequestStatus.NotReviewed);
        builder.HasOne(t => t.User).WithMany(a => a.Requests).HasForeignKey(t => t.UserId);
    }
}
