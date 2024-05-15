using AutoMapper;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, GetOrderResponseDto>()
            .ForMember(e => e.IsCommitedString, o => o.MapFrom(s => s.IsCommited ? "Завершенная" : "Незавершенная"));
    }
}
