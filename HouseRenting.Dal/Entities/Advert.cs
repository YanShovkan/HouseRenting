using HouseRenting.Common.Enums;

namespace HouseRenting.Dal.Entities;

public class Advert : IEntity
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public int Floor { get; set; }

    public int RoomsCount { get; set; }

    public decimal Price { get; set; }

    public decimal Area { get; set; }

    public string Address { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow.AddHours(4);

    public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotReviewed;

    public bool IsActual { get; set; } = true;

    public List<string> MediaFiles { get; set; } = new();

    public string ClientFIO { get; set; } = null!;

    public string ClientEmail { get; set; } = null!;

    public string? Comment { get; set; }

    public virtual List<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; } 
}

