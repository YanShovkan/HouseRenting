using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.User;

public record GetUserRequestDto(Guid? Id, string? Login) : IQueryDto;

