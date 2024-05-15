using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Enums;
using Refit;

namespace HouseRenting.Common.RefitContracts;

public interface IAdvertsClient
{
    [Get("/adverts/{id}")]
    public Task<GetAdvertResponseDto> GetAdvert(Guid id);

    [Post("/adverts/list")]
    public Task<GetAdvertsListResponseDto> GetAdverts([Body] GetAdvertsListRequestDto filterModel);

    [Patch("/adverts/managed/{id}")]
    public Task SetManagedStatusAdvert(Guid id, [Body] bool IsReviewed);

    [Post("/adverts/")]
    public Task CreateAdvert([Body] CreateAdvertRequestDto model);

    [Put("/adverts/")]
    public Task UpdateAdvert([Body] UpdateAdvertRequestDto model);

    [Delete("/adverts/{id}")]
    public Task DeleteAdvert(Guid id);
}
