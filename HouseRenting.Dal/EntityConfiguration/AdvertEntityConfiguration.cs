using HouseRenting.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HouseRenting.Common.Enums;

namespace HouseRenting.Dal.EntityConfiguration;
internal class AdvertEntityConfiguration : IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        builder.ToTable("adverts");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Floor).IsRequired();
        builder.Property(t => t.Price).IsRequired();
        builder.Property(t => t.Address).IsRequired(); 
        builder.Property(t => t.ReviewStatus).IsRequired().HasDefaultValue(ReviewStatus.NotReviewed);
        builder.Property(t => t.IsActual).IsRequired().HasDefaultValue(true);
        builder.Property(t => t.CreatedOn).IsRequired();
        builder.Property(t => t.RoomsCount).IsRequired();
        builder.Property(t => t.MediaFiles).HasColumnType("jsonb").IsRequired(false);
        builder.HasMany(t => t.Orders).WithOne(o => o.Advert);
        builder.HasOne(t => t.User).WithMany(a => a.Adverts).HasForeignKey(t => t.UserId);
    }
}
