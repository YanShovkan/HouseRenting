using AutoMapper;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<CreateAdminRequestDto, Admin>();
        CreateMap<UpdateAdminRequestDto, Admin>();
        CreateMap<Admin, GetAdminResponseDto>();
    }
}