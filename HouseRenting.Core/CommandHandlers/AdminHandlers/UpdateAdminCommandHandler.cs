using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdminHandlers;

public class UpdateAdminCommandHandler : ICommandHandler<UpdateAdminRequestDto>
{
    private readonly ICommandRepository<Admin> _adminCommandRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAdminCommandHandler(ICommandRepository<Admin> adminCommandRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _adminCommandRepository = adminCommandRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(UpdateAdminRequestDto model)
    {
        var admin = _mapper.Map<Admin>(model);
        _adminCommandRepository.Update(admin);
        await _unitOfWork.SaveChangesAsync();
    }
}

