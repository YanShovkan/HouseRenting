using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;

namespace HouseRenting.Core.QueryHandlers.AdminHandlers;

public class GetAdminQueryHandler : IQueryHandler<GetAdminRequestDto, GetAdminResponseDto>
{
    private readonly IQueryRepository<Admin> _adminQueryRepository;
    private readonly IMapper _mapper;

    public GetAdminQueryHandler(IQueryRepository<Admin> adminQueryRepository, IMapper mapper)
    {
        _adminQueryRepository = adminQueryRepository;
        _mapper = mapper;
    }

    public async Task<GetAdminResponseDto> HandleAsync(GetAdminRequestDto model)
    {
        GetAdminResponseDto admin = null!;

        if (model.Id is not null)
        {
            var userEntity = await _adminQueryRepository.GetByIdAsync(model.Id.GetValueOrDefault());
            admin = _mapper.Map<GetAdminResponseDto>(userEntity);
        }
        else if (model.Login is not null)
        {
            var userEntity = _adminQueryRepository.GetQuery().FirstOrDefault(u => u.Login.Equals(model.Login));
            admin = _mapper.Map<GetAdminResponseDto>(userEntity);
        }

        return admin;
    }
}