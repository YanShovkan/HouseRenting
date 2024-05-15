using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Order;

public class GetOrdersResposeDto : IQueryDto
{
    public IReadOnlyCollection<GetOrderResponseDto> Orders { get; set; } = new List<GetOrderResponseDto>();
}
