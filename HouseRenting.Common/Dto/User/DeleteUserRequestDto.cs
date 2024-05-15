using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.User;

public record DeleteUserRequestDto(Guid Id) : ICommandDto;
