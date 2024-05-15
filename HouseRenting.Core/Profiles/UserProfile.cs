using AutoMapper;
using HouseRenting.Common.Dto.User;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile() 
    {
        CreateMap<CreateUserRequestDto, User>();
        CreateMap<UpdateUserRequestDto, User>();
        CreateMap<User, GetUserResponseDto>();

    }
}
