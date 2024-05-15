using AutoMapper;
using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.Enums;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Profiles;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<CreateRequestRequestDto, Request>();
        CreateMap<Request, GetRequestResponseDto>()
            .ForMember(e => e.RequestStatusString, o => o.MapFrom(s => s.Status == RequestStatus.NotReviewed ? "Не рассмотрена" :
            s.Status == RequestStatus.InWork ? "В работе" :
            s.Status == RequestStatus.Denied ? "Отклонена" : "Квартира подобрана"));
    }
}