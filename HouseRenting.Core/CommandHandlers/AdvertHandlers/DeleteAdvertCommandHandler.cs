using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdvertHandlers;

public class DeleteAdvertCommandHandler : ICommandHandler<DeleteAdvertRequestDto>
{
    private readonly ICommandRepository<Advert> _advertCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAdvertCommandHandler(ICommandRepository<Advert> advertCommandRepository, IUnitOfWork unitOfWork)
    {
        _advertCommandRepository = advertCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(DeleteAdvertRequestDto model)
    {
        var advert = await _advertCommandRepository.GetByIdAsync(model.Id);
        _advertCommandRepository.Delete(advert!);
        await _unitOfWork.SaveChangesAsync();
    }
}
