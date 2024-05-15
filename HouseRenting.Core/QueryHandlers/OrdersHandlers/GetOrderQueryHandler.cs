using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.QueryHandlers.OrdersHandlers;
internal class GetOrderQueryHandler : IQueryHandler<GetOrderRequestDto, GetOrderResponseDto>
{
    private readonly IQueryRepository<Order> _ordertQueryRepository;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IQueryRepository<Order> orderQueryRepository, IMapper mapper)
    {
        _ordertQueryRepository = orderQueryRepository;
        _mapper = mapper;
    }

    public Task<GetOrderResponseDto> HandleAsync(GetOrderRequestDto model)
    {
        var orderEntity = _ordertQueryRepository.GetQuery().Where(_ => _.Id == model.Id).Include(_ => _.Admin).Include(_ => _.Advert).First();
        var order = _mapper.Map<GetOrderResponseDto>(orderEntity);

        return Task.FromResult(order);
    }
}
