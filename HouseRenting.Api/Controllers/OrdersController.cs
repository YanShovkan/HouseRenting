using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Api.Controllers;


[ApiController]
[Route("orders")]
public class OrdersController : Controller, IOrderClient
{
    private readonly IQueryHandler<GetOrderRequestDto, GetOrderResponseDto> _getOrderQueryHandler;
    private readonly IQueryHandler<GetOrderRequestDto, GetOrdersResposeDto> _getOrdersQueryHandler;
    private readonly ICommandHandler<CreateOrderRequestDto> _createOrderCommandHandler;
    private readonly ICommandHandler<DeleteOrderRequestDto> _deleteOrderCommandHandler;
    private readonly ICommandHandler<UpdateOrderRequestDto> _updateOrderCommandHandler;
    private readonly ICommandHandler<CommitOrderRequestDto> _commitOrderCommandHandler;

    public OrdersController(IQueryHandler<GetOrderRequestDto, GetOrderResponseDto> getOrderQueryHandler,
                            IQueryHandler<GetOrderRequestDto, GetOrdersResposeDto> getOrdersQueryHandler,
                            ICommandHandler<CreateOrderRequestDto> createOrderCommandHandler,
                            ICommandHandler<DeleteOrderRequestDto> deleteOrderCommandHandler,
                            ICommandHandler<UpdateOrderRequestDto> updateOrderCommandHandler,
                            ICommandHandler<CommitOrderRequestDto> commitOrderCommandHandler)
    {
        _getOrderQueryHandler = getOrderQueryHandler;
        _getOrdersQueryHandler = getOrdersQueryHandler;
        _createOrderCommandHandler = createOrderCommandHandler;
        _deleteOrderCommandHandler = deleteOrderCommandHandler;
        _updateOrderCommandHandler = updateOrderCommandHandler;
        _commitOrderCommandHandler = commitOrderCommandHandler;
    }

    [HttpGet("{id}")]
    public Task<GetOrderResponseDto> GetOrder([FromRoute] Guid id)
        => _getOrderQueryHandler.HandleAsync(new(id));

    [HttpPost()]
    public Task CreateOrder([FromBody] CreateOrderRequestDto model) =>
        _createOrderCommandHandler.HandleAsync(model);

    [HttpPut()]
    public Task UpdateOrder([FromBody] UpdateOrderRequestDto model) =>
        _updateOrderCommandHandler.HandleAsync(model);

    [HttpPatch("{id}")]
    public Task CommitOrder([FromRoute] Guid id) =>
        _commitOrderCommandHandler.HandleAsync(new(id));

    [HttpGet("list")]
    public async Task<GetOrdersResposeDto> GetOrders([FromQuery] Guid Id) =>
        await _getOrdersQueryHandler.HandleAsync(new(Id));

    [HttpDelete("{id}")]
    public Task DeleteOrder([FromRoute] Guid id) =>
        _deleteOrderCommandHandler.HandleAsync(new(id));
}
