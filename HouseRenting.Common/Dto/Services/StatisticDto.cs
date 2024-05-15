namespace HouseRenting.Common.Dto.Services;

public record StatisticDto
{
    public string Name { get; set; } = null!;

    public int Count { get; set; }
}
