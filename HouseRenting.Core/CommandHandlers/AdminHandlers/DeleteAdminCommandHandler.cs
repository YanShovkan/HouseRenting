using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.AdminHandlers;
internal class DeleteAdminCommandHandler : ICommandHandler<DeleteAdminRequestDto>
{
    private readonly ICommandRepository<Admin> _adminCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAdminCommandHandler(ICommandRepository<Admin> adminCommandRepository, IUnitOfWork unitOfWork)
    {
        _adminCommandRepository = adminCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(DeleteAdminRequestDto model)
    {
        var admin = await _adminCommandRepository.GetByIdAsync(model.Id);
        _adminCommandRepository.Delete(admin!);
        await _unitOfWork.SaveChangesAsync();
    }
}