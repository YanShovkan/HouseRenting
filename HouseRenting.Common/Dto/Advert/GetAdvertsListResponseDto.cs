using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Advert;

public record GetAdvertsListResponseDto(IReadOnlyCollection<GetAdvertResponseDto> Adverts) : IQueryDto;
