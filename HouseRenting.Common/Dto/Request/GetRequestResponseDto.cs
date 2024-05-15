using HouseRenting.Common.Dto.User;
using HouseRenting.Common.Enums;

namespace HouseRenting.Common.Dto.Request;

public record GetRequestResponseDto
{
    public Guid Id { get; set; }

    public decimal? PriceLess { get; set; }

    public decimal? PriceGreater { get; set; }

    public decimal? AreaLess { get; set; }

    public decimal? AreaGreater { get; set; }

    public int? RoomsCountLess { get; set; }

    public int? RoomsCountGreater { get; set; }

    public RequestStatus Status { get; set; }

    public string RequestStatusString { get; set; } = null!;

    public GetUserResponseDto User { get; set; } = null!;

    public int MonthCount { get; set; }

    public string? Comment { get; set; }
}
