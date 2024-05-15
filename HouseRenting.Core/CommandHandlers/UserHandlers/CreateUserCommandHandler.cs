using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.UserHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserRequestDto>
{
    private readonly ICommandRepository<User> _userCommandRepository;
    private readonly IQueryRepository<User> _userQueryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandHandler(ICommandRepository<User> userCommandRepository, IQueryRepository<User> userQueryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreateUserRequestDto model)
    {
        var userCheck = _userQueryRepository.GetQuery().FirstOrDefault(u => u.Login.Equals(model.Login));

        if (userCheck is not null)
        {
            throw new Exception("Пользователь с таким логином уже существует.");
        }

        var user = _mapper.Map<User>(model);
        await _userCommandRepository.CreateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
