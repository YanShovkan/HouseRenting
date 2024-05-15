using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.UserHandlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserRequestDto>
{
    private readonly ICommandRepository<User> _userCommandRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserCommandHandler(ICommandRepository<User> userCommandRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(UpdateUserRequestDto model)
    {
        var user = _mapper.Map<User>(model);
        _userCommandRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
