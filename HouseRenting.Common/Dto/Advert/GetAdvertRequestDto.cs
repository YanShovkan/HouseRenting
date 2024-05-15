using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Advert;

public record GetAdvertRequestDto(Guid Id) : IQueryDto;
