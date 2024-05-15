using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.OrderHandlers;

public class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderRequestDto>
{
    private readonly ICommandRepository<Order> _orderRepository;
    private readonly ICommandRepository<Advert> _advertRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(ICommandRepository<Order> orderRepository,
                                     ICommandRepository<Advert> advertRepository,
                                     IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _advertRepository = advertRepository;
    }

    public async Task HandleAsync(DeleteOrderRequestDto model)
    {
        var order = await _orderRepository.GetByIdAsync(model.Id);
        var advert = await _advertRepository.GetByIdAsync(order!.AdvertId);
        advert!.IsActual = true;
        _advertRepository.Update(advert!);
        _orderRepository.Delete(order!);
        await _unitOfWork.SaveChangesAsync();
    }
}

