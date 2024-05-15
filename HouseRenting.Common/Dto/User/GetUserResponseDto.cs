using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.User;

public record GetUserResponseDto(Guid Id, string Name, string Login, string Password, string Email) : IQueryDto;
