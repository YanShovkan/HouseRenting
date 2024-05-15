using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Core.CommandHandlers.OrderHandlers;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderRequestDto>
{
    private readonly ICommandRepository<Order> _orderCommandRepository;
    private readonly ICommandRepository<Advert> _advertCommandRepository;
    private readonly IQueryRepository<Advert> _advertQueryRepository;
    private readonly IQueryRepository<Admin> _adminQueryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    public CreateOrderCommandHandler(ICommandRepository<Order> orderCommandRepository,
                                     ICommandRepository<Advert> advertCommandRepository,
                                     IQueryRepository<Advert> advertQueryRepository,
                                     IQueryRepository<Admin> adminQueryRepository,
                                     IUnitOfWork unitOfWork,
                                     IEmailSender emailSender)
    {
        _orderCommandRepository = orderCommandRepository;
        _advertCommandRepository = advertCommandRepository;
        _advertQueryRepository = advertQueryRepository;
        _adminQueryRepository = adminQueryRepository;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }
    public async Task HandleAsync(CreateOrderRequestDto model)
    {
        var admin = await _adminQueryRepository.GetByIdAsync(model.AdminId);
        
        if(admin is null)
        {
            throw new Exception("Администратор не найден");
        }

        var advert = await _advertQueryRepository.GetByIdAsync(model.AdvertId);
        
        if (advert is null)
        {
            throw new Exception("Объявление не найдено");
        }

        advert.IsActual = false;

        var order = new Order
        {
            AdminId = model.AdminId,
            AdvertId = model.AdvertId,
            StartDate = model.StartDate.ToUniversalTime().AddHours(4),
            EndDate = model.StartDate.AddMonths(model.MounthCount).ToUniversalTime().AddHours(4),
            TotalSum = model.MounthCount * model.MounthPrice,
            AgencySum = model.MounthCount * model.MounthPrice * model.AgencyPercent / 100,
            AgencyPercent = model.AgencyPercent,
            MounthCount = model.MounthCount,
            MounthPrice = model.MounthPrice,
            MeetTime = model.MeetTime.ToUniversalTime().AddHours(4)
        };

        await _orderCommandRepository.CreateAsync(order);
        _advertCommandRepository.Update(advert);

        _emailSender.SendEmailAdoutMeet(order.MeetTime, advert, admin, true);
        await _unitOfWork.SaveChangesAsync();
    }
}
