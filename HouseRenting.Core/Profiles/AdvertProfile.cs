using AutoMapper;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Enums;
using HouseRenting.Dal.Entities;

namespace HouseRenting.Core.Profiles;

public class AdvertProfile : Profile
{
    public AdvertProfile()
    {
        CreateMap<CreateAdvertRequestDto, Advert>();
        CreateMap<UpdateAdvertRequestDto, Advert>()
            .ForMember(e => e.ReviewStatus, o => o.MapFrom(s => s.IsAdminReviewed ? ReviewStatus.Reviewed : ReviewStatus.NotReviewed));
        CreateMap<Advert, GetAdvertResponseDto>()
            .ForMember(e => e.IsActualString, o => o.MapFrom(s => s.IsActual ? "Не сдана" : "Сдана"))
            .ForMember(e => e.ReviewStatusString, o => o.MapFrom(s => s.ReviewStatus == ReviewStatus.Reviewed ? "Подтвержденное" : s.ReviewStatus == ReviewStatus.NotReviewed ? "Не подтвержденное" : "Отклоненное"));
    }
}
