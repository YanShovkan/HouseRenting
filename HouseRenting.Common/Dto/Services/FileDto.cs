namespace HouseRenting.Common.Dto.Services;

public record FileDto
{
    public List<string> Files { get; set; } = new();
}
