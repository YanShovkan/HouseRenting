using HouseRenting.Common.CQRS;
using HouseRenting.Common.Enums;

namespace HouseRenting.Common.Dto.Advert;

public record UpdateAdvertRequestDto() : ICommandDto
{
    public Guid Id { get; set; }
    public int Floor { get; set; }
    public int RoomsCount { get; set; }
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public bool IsAdminReviewed { get; set; }
    public string Address { get; set; } = null!;
    public string ClientFIO { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public List<string> MediaFiles { get; set; } = new();
    public DateTime CreatedOn { get; set; }
    public Guid? UserId { get; set; }
    public string? Comment { get; set; }
}
