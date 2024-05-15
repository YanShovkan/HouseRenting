using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Common.Dto.Advert;

namespace HouseRenting.Common.Dto.Order;

public record GetOrderResponseDto() : IQueryDto
{
    public Guid Id { get; set; }

    public DateTime MeetTime { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal AgencyPercent { get; set; }

    public int MounthCount { get; set; }

    public decimal MounthPrice { get; set; }

    public decimal AgencySum { get; set; }

    public decimal TotalSum { get; set; }

    public DateTime? CommitedDate { get; set; }

    public bool IsCommited { get; set; }
    
    public string IsCommitedString { get; set; } = null!;

    public GetAdminResponseDto Admin { get; set; } = null!;

    public GetAdvertResponseDto Advert { get; set; } = null!;
}
