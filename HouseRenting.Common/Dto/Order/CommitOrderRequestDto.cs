using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Order;
public record CommitOrderRequestDto(Guid Id) : ICommandDto;
