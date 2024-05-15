namespace HouseRenting.Dal.Entities;

public class Order : IEntity
{
    public Guid Id { get; set; }
    public Guid AdminId { get; set; }
    public Guid AdvertId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime MeetTime { get; set; }

    public decimal AgencyPercent { get; set; }
    
    public int MounthCount { get; set; }

    public decimal MounthPrice { get; set; }

    public decimal AgencySum { get; set; }
    
    public decimal TotalSum { get; set; }

    public DateTime? CommitedDate { get; set; }

    public bool IsCommited { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual Advert Advert { get; set; } = null!;
}
