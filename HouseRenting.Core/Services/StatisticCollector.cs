using HouseRenting.Common.Dto.Services;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Core.Services;

public class StatisticCollector : IStatisticCollector
{
    private readonly IQueryRepository<Advert> _queryRepository;

    public StatisticCollector(IQueryRepository<Advert> queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<RentStatisticDto> GetStatistic()
    {
        var adverts = await _queryRepository.GetQuery().Where(_ => _.IsActual && _.ReviewStatus == Common.Enums.ReviewStatus.Reviewed).GroupBy(_ => _.RoomsCount).Select(_ => new StatisticDto { Name = _.Key == 0 ? "квартиры студии" : $"{_.Key} комнатные квартиры", Count = _.Count() }).ToArrayAsync();
        var response = new RentStatisticDto();
        response.Statistics.AddRange(adverts);
        return response;
    }
}
