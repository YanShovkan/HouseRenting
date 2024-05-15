using HouseRenting.Common.Dto.Order;
using Refit;

namespace HouseRenting.Common.RefitContracts;
public interface IOrderClient
{
    [Get("/orders/{id}")]
    public Task<GetOrderResponseDto> GetOrder(Guid id);

    [Post("/orders")]
    public Task CreateOrder([Body] CreateOrderRequestDto model);
    
    [Delete("/orders/{id}")]
    public Task DeleteOrder(Guid id);

    [Put("/orders")]
    public Task UpdateOrder([Body] UpdateOrderRequestDto model);

    [Get("/orders/list")]
    public Task<GetOrdersResposeDto> GetOrders([Query] Guid Id);

    [Patch("/orders/{id}")]
    public Task CommitOrder(Guid id);
}
