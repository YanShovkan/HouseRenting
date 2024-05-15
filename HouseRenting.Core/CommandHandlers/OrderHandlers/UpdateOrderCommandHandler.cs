using AutoMapper;
using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Core.CommandHandlers.OrderHandlers;
public class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderRequestDto>
{
    private readonly ICommandRepository<Order> _orderCommandRepository;
    private readonly ICommandRepository<Advert> _advertCommandRepository;
    private readonly IQueryRepository<Order> _orderQueryRepository;
    private readonly IQueryRepository<Advert> _advertQueryRepository;
    private readonly IQueryRepository<Admin> _adminQueryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;


    public UpdateOrderCommandHandler(ICommandRepository<Order> orderCommandRepository,
                                     ICommandRepository<Advert> advertCommandRepository,
                                     IQueryRepository<Order> orderQueryRepository,
                                     IQueryRepository<Advert> advertQueryRepository,
                                     IQueryRepository<Admin> adminQueryRepository,
                                     IUnitOfWork unitOfWork,
                                     IEmailSender emailSender)
    {
        _orderCommandRepository = orderCommandRepository;
        _advertCommandRepository = advertCommandRepository;
        _advertQueryRepository = advertQueryRepository;
        _adminQueryRepository = adminQueryRepository;
        _orderQueryRepository = orderQueryRepository;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }

    public async Task HandleAsync(UpdateOrderRequestDto model)
    {
        var admin = await _adminQueryRepository.GetByIdAsync(model.AdminId);

        if (admin is null)
        {
            throw new Exception("Администратор не найден");
        }

        if (model.OldAdvertId != model.AdvertId)
        {
            var oldAdvert = await _advertQueryRepository.GetByIdAsync(model.OldAdvertId);
            var advert = await _advertQueryRepository.GetByIdAsync(model.AdvertId);

            if (advert is null || oldAdvert is null)
            {
                throw new Exception("Объявление не найдено");
            }

            advert.IsActual = false;
            oldAdvert.IsActual = true;
            _advertCommandRepository.Update(advert);
            _advertCommandRepository.Update(oldAdvert);
        }

        var order = await _orderQueryRepository.GetQuery().Include(_ => _.Advert).Include(_ => _.Admin).FirstOrDefaultAsync(_ => _.Id == model.Id);

        if (order is null)
        {
            throw new Exception("Заявка не найдена");
        }
        
        var oldDate = order.MeetTime;

        order.AdvertId = model.AdvertId;
        order.StartDate = model.StartDate.ToUniversalTime();
        order.EndDate = model.StartDate.AddMonths(model.MounthCount).ToUniversalTime();
        order.TotalSum = model.MounthCount * model.MounthPrice;
        order.AgencySum = model.MounthCount * model.MounthPrice * model.AgencyPercent / 100;
        order.AgencyPercent = model.AgencyPercent;
        order.MounthCount = model.MounthCount;
        order.MounthPrice = model.MounthPrice;
        order.MeetTime = model.MeetTime.ToUniversalTime();

        if(order.MeetTime != model.MeetTime)
        {
            _emailSender.SendEmailAdoutMeet(order.MeetTime, order.Advert, order.Admin, false, oldDate);
            order.MeetTime = order.MeetTime.AddHours(4);
        }
        if (order.StartDate != model.StartDate)
        {
            order.StartDate = order.StartDate.AddHours(4);
            order.EndDate = order.EndDate.AddHours(4);
        }

        _orderCommandRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
    }
}
