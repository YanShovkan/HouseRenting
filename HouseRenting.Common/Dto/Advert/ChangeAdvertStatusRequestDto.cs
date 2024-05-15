using HouseRenting.Common.CQRS;
using HouseRenting.Common.Enums;

namespace HouseRenting.Common.Dto.Advert;

public record ChangeAdvertStatusRequestDto(Guid Id, ReviewStatus ReviewStatus) : ICommandDto;
