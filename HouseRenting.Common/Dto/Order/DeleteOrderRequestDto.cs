using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Order;

public record DeleteOrderRequestDto(Guid Id) : ICommandDto;