using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;

namespace HouseRenting.Core.QueryHandlers.AdvertHandlers;

public class GetAdvertsListQueryHandler : IQueryHandler<GetAdvertsListRequestDto, GetAdvertsListResponseDto>
{
    private readonly IQueryRepository<Advert> _advertQueryRepository;
    private readonly IMapper _mapper;

    public GetAdvertsListQueryHandler(IQueryRepository<Advert> advertQueryRepository, IMapper mapper)
    {
        _advertQueryRepository = advertQueryRepository;
        _mapper = mapper;
    }

    public async Task<GetAdvertsListResponseDto> HandleAsync(GetAdvertsListRequestDto model)
    {
        IReadOnlyCollection<Advert> advertsListEntities = await _advertQueryRepository.GetAllAsync();
        var advertsList = _mapper.Map<IReadOnlyCollection<GetAdvertResponseDto>>(advertsListEntities);

        return new(advertsList);
    }
}