using HouseRenting.Common.Dto.Services;
using Refit;

namespace HouseRenting.Common.RefitContracts;
public interface IServicesClient
{
    [Post("/services/avitofile")]
    public Task<byte[]> GetReport();

    [Post("/services/report")]
    public Task<byte[]> GetReport([Body] GenerateReportDto model);

    [Post("/services/predict/{id}")]
    public Task<PredictDto> GetPredict(Guid Id);

    [Get("/services/statistic")]
    public Task<RentStatisticDto> GetStatistic();
}
