namespace HouseRenting.Common.Dto.Services;
public record RentStatisticDto
{
    public List<StatisticDto> Statistics { get; set; } = new();
}
