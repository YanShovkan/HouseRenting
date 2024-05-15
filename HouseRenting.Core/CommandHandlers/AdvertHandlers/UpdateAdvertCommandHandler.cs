using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdvertHandlers;

public class UpdateAdvertCommandHandler : ICommandHandler<UpdateAdvertRequestDto>
{
    private readonly ICommandRepository<Advert> _advertCommandRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAdvertCommandHandler(ICommandRepository<Advert> advertCommandRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _advertCommandRepository = advertCommandRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(UpdateAdvertRequestDto model)
    {
        var advert = _mapper.Map<Advert>(model);
        _advertCommandRepository.Update(advert);
        await _unitOfWork.SaveChangesAsync();
    }
}
