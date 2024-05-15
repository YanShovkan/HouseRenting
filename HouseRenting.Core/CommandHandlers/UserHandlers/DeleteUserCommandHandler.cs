using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.UserHandlers;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserRequestDto>
{
    private readonly ICommandRepository<User> _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(ICommandRepository<User> userCommandRepository, IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task HandleAsync(DeleteUserRequestDto model)
    {
        var user = await _userCommandRepository.GetByIdAsync(model.Id);
        _userCommandRepository.Delete(user!);
        await _unitOfWork.SaveChangesAsync();
    }
}
