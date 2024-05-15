﻿using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Order;

public class UpdateOrderRequestDto : ICommandDto
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public decimal AgencyPercent { get; set; }
    public int MounthCount { get; set; }
    public decimal MounthPrice { get; set; }
    public Guid AdvertId { get; set; }
    public Guid OldAdvertId { get; set; }
    public Guid AdminId { get; set; }
    public DateTime MeetTime { get; set; }
}
