using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.QueryHandlers.OrdersHandlers;
public class GetOrdersQueryHandler : IQueryHandler<GetOrderRequestDto, GetOrdersResposeDto>
{
    private readonly IQueryRepository<Order> _ordertQueryRepository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IQueryRepository<Order> orderQueryRepository, IMapper mapper)
    {
        _ordertQueryRepository = orderQueryRepository;
        _mapper = mapper;
    }

    public async Task<GetOrdersResposeDto> HandleAsync(GetOrderRequestDto model)
    {
        var ordersEntities = await _ordertQueryRepository.GetQuery().Where(_ => _.AdminId == model.Id).Include(_ => _.Admin).Include(_ => _.Advert).ToArrayAsync();
        var orders = _mapper.Map<List<GetOrderResponseDto>>(ordersEntities);
        var response = new GetOrdersResposeDto { Orders = orders };
        return response;
    }
}
