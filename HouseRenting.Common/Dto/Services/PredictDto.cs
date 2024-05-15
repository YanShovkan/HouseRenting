namespace HouseRenting.Common.Dto.Services;

public record PredictDto
{
    public string AdminName { get; set; } = string.Empty;

    public Dictionary<string, decimal> MonthOrders { get; set; } = new();   

    public decimal Predict { get; set; }
}
