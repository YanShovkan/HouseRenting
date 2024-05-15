using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Order;

public record GetOrderRequestDto(Guid Id) : IQueryDto;
