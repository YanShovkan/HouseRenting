using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Advert;

public record CreateAdvertRequestDto() : ICommandDto
{
    public int Floor { get; set; }
    public int RoomsCount { get; set; }
    public decimal Price { get; set; }
    public decimal Area { get; set; }
    public string Address { get; set; } = null!;
    public string ClientFIO { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public List<string> MediaFiles { get; set; } = new();
    public Guid? UserId { get; set; }
    public string? Comment { get; set; }
}
