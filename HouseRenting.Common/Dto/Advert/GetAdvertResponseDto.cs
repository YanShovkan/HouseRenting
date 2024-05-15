using HouseRenting.Common.CQRS;
using HouseRenting.Common.Enums;

namespace HouseRenting.Common.Dto.Advert;

public record GetAdvertResponseDto() : IQueryDto
{
    public Guid Id { get; set; }
    public int Floor { get; set; }
    public int RoomsCount { get; set; }
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Address { get; set; } = null!;
    public ReviewStatus ReviewStatus { get; set; }
    public string ReviewStatusString { get; set; } = null!;
    public bool IsActual { get; set; }
    public string IsActualString { get; set; } = null!;
    public List<string> MediaFiles { get; set; } = new();
    public string ClientFIO { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public Guid UserId { get; set; }
    public string? Comment { get; set; }
}