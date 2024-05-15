
using HouseRenting.Common.Enums;

namespace HouseRenting.Dal.Entities;

public class Request : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal? PriceLess { get; set; }

    public decimal? PriceGreater { get; set; }

    public decimal? AreaLess { get; set; }

    public decimal? AreaGreater { get; set; }

    public int? RoomsCountLess { get; set; }

    public int? RoomsCountGreater { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.NotReviewed;
    
    public int MonthCount { get; set; }

    public virtual User User { get; set; } = null!;

    public string? Comment { get; set; }
}
