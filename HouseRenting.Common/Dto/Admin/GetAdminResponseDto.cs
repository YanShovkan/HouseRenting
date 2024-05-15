using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Admin;

public record GetAdminResponseDto(Guid Id, string Name, string Login, string Password, string Email) : IQueryDto;
