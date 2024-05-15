using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Admin;

public record GetAdminRequestDto(Guid? Id, string? Login) : IQueryDto;

