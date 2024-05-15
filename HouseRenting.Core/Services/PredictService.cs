using HouseRenting.Common;
using HouseRenting.Common.Dto.Services;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.Services;
public class PredictService : IPredictService
{
    private readonly IQueryRepository<Order> _orderRepository;

    public PredictService(IQueryRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PredictDto> GetPredict(Guid Id)
    {
        var orders = await _orderRepository.GetQuery().Where(_ => _.IsCommited && _.CommitedDate >= DateTime.UtcNow.AddYears(-1) && _.AdminId == Id).Include(_ => _.Admin).OrderByDescending(_ => _.CommitedDate).ToArrayAsync();
             
        var result = new PredictDto()
        {
            AdminName = orders.First().Admin.Name
        };
        var currentMonth = DateTime.UtcNow.Month;

        for(int i = currentMonth + 1; i <= 12; i++) 
        {
            foreach(var order in orders.Where(_ => _.CommitedDate!.Value.Month == i))
            {
                if(!result.MonthOrders.TryAdd(MonthHelper.GetMonthName(i), order.AgencySum))
                {
                    result.MonthOrders[MonthHelper.GetMonthName(i)] =+ order.AgencySum;
                }
            }
        }

        for (int i = 0; i < currentMonth; i++)
        {
            foreach (var order in orders.Where(_ => _.CommitedDate!.Value.Month == i))
            {
                if (!result.MonthOrders.TryAdd(MonthHelper.GetMonthName(i), order.AgencySum))
                {
                    result.MonthOrders[MonthHelper.GetMonthName(i)] += order.AgencySum;
                }
            }
        }

        var predict = result.MonthOrders.Values.Average();
        result.Predict = predict;

        foreach (var order in orders.Where(_ => _.CommitedDate!.Value.Month == currentMonth && _.CommitedDate.Value.Year == DateTime.UtcNow.Year))
        {
            if (!result.MonthOrders.TryAdd(MonthHelper.GetMonthName(currentMonth), order.AgencySum))
            {
                result.MonthOrders[MonthHelper.GetMonthName(currentMonth)] += order.AgencySum;
            }
        }

        return result;
    }
}
