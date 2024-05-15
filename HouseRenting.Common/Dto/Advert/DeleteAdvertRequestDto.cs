using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Advert;

public record DeleteAdvertRequestDto(Guid Id) : ICommandDto;