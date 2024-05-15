using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdminHandlers;

public class CreateAdminCommandHandler : ICommandHandler<CreateAdminRequestDto>
{
    private readonly ICommandRepository<Admin> _adminCommandRepository;
    private readonly IQueryRepository<Admin> _adminQueryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdminCommandHandler(ICommandRepository<Admin> adminCommandRepository, IQueryRepository<Admin> adminQueryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _adminCommandRepository = adminCommandRepository;
        _adminQueryRepository = adminQueryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreateAdminRequestDto model)
    {
        var adminCheck = _adminQueryRepository.GetQuery().FirstOrDefault(u => u.Login.Equals(model.Login));

        if (adminCheck is not null)
        {
            throw new Exception("Администратор с таким логином уже существует.");
        }

        var admin = _mapper.Map<Admin>(model);
        await _adminCommandRepository.CreateAsync(admin);
        await _unitOfWork.SaveChangesAsync();
    }
}
