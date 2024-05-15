using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdvertHandlers;

public class CreateAdvertCommandHandler : ICommandHandler<CreateAdvertRequestDto>
{
    private readonly ICommandRepository<Advert> _advertCommandRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdvertCommandHandler(ICommandRepository<Advert> advertCommandRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _advertCommandRepository = advertCommandRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreateAdvertRequestDto model)
    {
        var advert = _mapper.Map<Advert>(model);
        
        if (model.IsAdmin)
        {
            advert.ReviewStatus = Common.Enums.ReviewStatus.Reviewed;
        }

        advert.CreatedOn = DateTime.UtcNow.AddHours(4);
        await _advertCommandRepository.CreateAsync(advert);
        await _unitOfWork.SaveChangesAsync();
    }
}
