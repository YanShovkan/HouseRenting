using HouseRenting.Common.Dto.Services;

namespace HouseRenting.Core.Services.Contracts;

public interface IStatisticCollector
{
    Task<RentStatisticDto> GetStatistic();
}
