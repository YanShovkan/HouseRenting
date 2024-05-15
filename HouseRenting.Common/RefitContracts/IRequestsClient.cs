using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace HouseRenting.Common.RefitContracts;
public interface IRequestsClient
{
    [Get("/requests/{id}")]
    public Task<GetRequestResponseDto> GetRequest(Guid id);

    [Post("/requests/list")]
    public Task<GetRequestsListResponseDto> GetRequests();

    [Post("/requests/")]
    public Task CreateRequest([Body] CreateRequestRequestDto model);

    [Delete("/requests/{id}")]
    public Task DeleteRequest(Guid id);

    [Patch("/requests/{id}")]
    public Task UpdateStatusRequest(Guid id, [Query] RequestStatus requestStatus);

    [Put("/requests/")]
    public Task UpdateRequest([FromBody] UpdateRequestRequestDto model);
}
