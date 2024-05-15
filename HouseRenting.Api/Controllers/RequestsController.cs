using AutoMapper;
using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.Enums;
using HouseRenting.Common.RefitContracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Api.Controllers;

[ApiController]
[Route("requests")]
public class RequestsController : Controller, IRequestsClient
{
    private readonly ICommandRepository<Request> _commandRepository;
    private readonly IQueryRepository<Request> _queryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RequestsController(ICommandRepository<Request> commandRepository,
                              IQueryRepository<Request> queryRepository,
                              IMapper mapper,
                              IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<GetRequestResponseDto> GetRequest([FromRoute] Guid id)
        => _mapper.Map<GetRequestResponseDto>(await _queryRepository.GetQuery().Include(_ => _.User).FirstAsync(_ => _.Id == id));

    [HttpPost("list")]
    public async Task<GetRequestsListResponseDto> GetRequests()
        => new GetRequestsListResponseDto() { Requests = _mapper.Map<List<GetRequestResponseDto>>(await _queryRepository.GetQuery().Include(_ => _.User).ToArrayAsync()) };

    [HttpPost()]
    public async Task CreateRequest([FromBody] CreateRequestRequestDto model)
    {
        var request = _mapper.Map<Request>(model);
        await _commandRepository.CreateAsync(request);
        await _unitOfWork.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeleteRequest([FromRoute] Guid id)
    {
        var request = await _queryRepository.GetByIdAsync(id);
        _commandRepository.Delete(request!);
        await _unitOfWork.SaveChangesAsync();
    }

    [HttpPatch("{id}")]
    public async Task UpdateStatusRequest(Guid id, [FromQuery] RequestStatus requestStatus)
    {
        var request = await _queryRepository.GetByIdAsync(id);
        request!.Status = requestStatus;
        _commandRepository.Update(request!);
        await _unitOfWork.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task UpdateRequest([FromBody] UpdateRequestRequestDto model)
    {
        var request = await _queryRepository.GetByIdAsync(model.Id);
        request!.PriceLess = model.PriceLess;
        request!.PriceGreater = model.PriceGreater;
        request!.AreaLess = model.AreaLess;
        request!.AreaGreater = model.AreaGreater;
        request!.RoomsCountGreater = model.RoomsCountGreater;
        request!.RoomsCountLess = model.RoomsCountLess;
        request!.MonthCount = model.MonthCount;
        request!.Comment = model.Comment;
        _commandRepository.Update(request!);
        await _unitOfWork.SaveChangesAsync();
    }
}
