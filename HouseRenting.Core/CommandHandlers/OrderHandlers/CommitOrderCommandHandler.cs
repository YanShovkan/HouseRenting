using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.OrderHandlers;

public class CommitOrderCommandHandler : ICommandHandler<CommitOrderRequestDto>
{
    private readonly ICommandRepository<Order> _orderCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommitOrderCommandHandler(ICommandRepository<Order> orderCommandRepository, IUnitOfWork unitOfWork)
    {
        _orderCommandRepository = orderCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CommitOrderRequestDto model)
    {
        var order = await _orderCommandRepository.GetByIdAsync(model.Id);

        if (order == null)
        {
            throw new Exception("Сделка не найдена");
        }

        order.CommitedDate = DateTime.UtcNow;
        order.IsCommited = true;
        _orderCommandRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
    }
}
