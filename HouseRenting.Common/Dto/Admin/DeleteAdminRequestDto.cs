using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Admin;

public record DeleteAdminRequestDto(Guid Id) : ICommandDto;
