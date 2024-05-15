namespace HouseRenting.Common.Dto.Services;

public class CommitedOrder
{
    public string AdminName { get; set; } = null!;

    public DateTime Date { get; set; }

    public decimal Sum { get; set; }

    public string Address { get; set; } = null!;
    
    public string ClientFio { get; set; } = null!;

    public string ClientMail { get; set; } = null!;
}
