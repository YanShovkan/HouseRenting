using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;

namespace HouseRenting.Core.QueryHandlers.AdvertHandlers;
internal class GetAdvertQueryHandler : IQueryHandler<GetAdvertRequestDto, GetAdvertResponseDto>
{
    private readonly IQueryRepository<Advert> _advertQueryRepository;
    private readonly IMapper _mapper;

    public GetAdvertQueryHandler(IQueryRepository<Advert> advertQueryRepository, IMapper mapper)
    {
        _advertQueryRepository = advertQueryRepository;
        _mapper = mapper;
    }

    public async Task<GetAdvertResponseDto> HandleAsync(GetAdvertRequestDto model)
    {
        var advertEntity = await _advertQueryRepository.GetByIdAsync(model.Id);
        var advert = _mapper.Map<GetAdvertResponseDto>(advertEntity);

        return advert;
    }
}