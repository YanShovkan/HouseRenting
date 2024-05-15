using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Enums;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Api.Controllers;

[ApiController]
[Route("adverts")]
public class AdvertsController : Controller, IAdvertsClient
{
    private readonly IQueryHandler<GetAdvertRequestDto, GetAdvertResponseDto> _advertQueryHandler;
    private readonly IQueryHandler<GetAdvertsListRequestDto, GetAdvertsListResponseDto> _advertListQueryHandler;
    private readonly ICommandHandler<CreateAdvertRequestDto> _createAdvertCommandHandler;
    private readonly ICommandHandler<UpdateAdvertRequestDto> _updateAdvertCommandHandler;
    private readonly ICommandHandler<DeleteAdvertRequestDto> _deleteAdvertCommandHandler;
    private readonly ICommandHandler<ChangeAdvertStatusRequestDto> _changeAdvertStatusCommandHandler;

    public AdvertsController(IQueryHandler<GetAdvertRequestDto, GetAdvertResponseDto> advertQueryHandler,
                            IQueryHandler<GetAdvertsListRequestDto, GetAdvertsListResponseDto> advertListQueryHandler,
                            ICommandHandler<CreateAdvertRequestDto> createAdvertCommandHandler,
                            ICommandHandler<UpdateAdvertRequestDto> updateAdvertCommandHandler,
                            ICommandHandler<DeleteAdvertRequestDto> deleteAdvertCommandHandler,
                            ICommandHandler<ChangeAdvertStatusRequestDto> changeAdvertStatusCommandHandler)
    {
        _advertQueryHandler = advertQueryHandler;
        _advertListQueryHandler = advertListQueryHandler;
        _createAdvertCommandHandler = createAdvertCommandHandler;
        _updateAdvertCommandHandler = updateAdvertCommandHandler;
        _deleteAdvertCommandHandler = deleteAdvertCommandHandler;
        _changeAdvertStatusCommandHandler = changeAdvertStatusCommandHandler;
    }

    [HttpGet("{id}")]
    public Task<GetAdvertResponseDto> GetAdvert([FromRoute] Guid id)
        => _advertQueryHandler.HandleAsync(new(id));

    [HttpPost("list")]
    public Task<GetAdvertsListResponseDto> GetAdverts([FromBody] GetAdvertsListRequestDto filterModel)
        => _advertListQueryHandler.HandleAsync(filterModel);

    [HttpPatch("managed/{id}")]
    public Task SetManagedStatusAdvert([FromRoute] Guid id, [FromBody] bool IsReviewed) =>
        _changeAdvertStatusCommandHandler.HandleAsync(new(id, IsReviewed ? ReviewStatus.Reviewed : ReviewStatus.Denied));

    [HttpPost()]
    public Task CreateAdvert([FromBody] CreateAdvertRequestDto model) =>
        _createAdvertCommandHandler.HandleAsync(model);

    [HttpPut()]
    public Task UpdateAdvert([FromBody] UpdateAdvertRequestDto model) =>
        _updateAdvertCommandHandler.HandleAsync(model);


    [HttpDelete("{id}")]
    public Task DeleteAdvert([FromRoute] Guid id) =>
        _deleteAdvertCommandHandler.HandleAsync(new(id));
}
