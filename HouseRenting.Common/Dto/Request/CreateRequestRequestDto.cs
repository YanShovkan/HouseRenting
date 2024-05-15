namespace HouseRenting.Common.Dto.Request;
public class CreateRequestRequestDto
{
    public Guid UserId { get; set; }

    public decimal? PriceLess { get; set; }

    public decimal? PriceGreater { get; set; }

    public decimal? AreaLess { get; set; }

    public decimal? AreaGreater { get; set; }

    public int? RoomsCountLess { get; set; }

    public int? RoomsCountGreater { get; set; }
    
    public int MonthCount { get; set; }

    public string? Comment { get; set; }
}
