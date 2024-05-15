using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;

namespace HouseRenting.Core.QueryHandlers.UserHandlers;

public class GetUserQueryHandler : IQueryHandler<GetUserRequestDto, GetUserResponseDto>
{
    private readonly IQueryRepository<User> _userQueryRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IQueryRepository<User> userQueryRepository, IMapper mapper)
    {
        _userQueryRepository = userQueryRepository;
        _mapper = mapper;
    }

    public async Task<GetUserResponseDto> HandleAsync(GetUserRequestDto model)
    {
        GetUserResponseDto user = null!;

        if (model.Id is not null)
        {
            var userEntity = await _userQueryRepository.GetByIdAsync(model.Id.GetValueOrDefault());
            user = _mapper.Map<GetUserResponseDto>(userEntity);
        }
        else if (model.Login is not null)
        {
            var userEntity = _userQueryRepository.GetQuery().FirstOrDefault(u => u.Login.Equals(model.Login));
            user = _mapper.Map<GetUserResponseDto>(userEntity);
        }

        return user;
    }
}
