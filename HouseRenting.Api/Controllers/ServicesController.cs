using HouseRenting.Common.Dto.Services;
using HouseRenting.Common.RefitContracts;
using HouseRenting.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Api.Controllers;

[ApiController]
[Route("services")]
public class ServicesController : Controller, IServicesClient
{
    private readonly IReportGenerator _reportGenerator;
    private readonly IPredictService _predictService;
    private readonly IStatisticCollector _statisticCollector;

    public ServicesController(IReportGenerator reportGenerator, IPredictService predictService, IStatisticCollector statisticCollector)
    {
        _reportGenerator = reportGenerator;
        _predictService = predictService;
        _statisticCollector = statisticCollector;
    }

    [HttpPost("avitofile")]
    public Task<byte[]> GetReport() =>
        _reportGenerator.CreateAvitoFileAsync();

    [HttpPost("report")]
    public Task<byte[]> GetReport([FromBody] GenerateReportDto model) =>
        _reportGenerator.CreateReportAsync(model);

    [HttpPost("predict/{id}")]
    public Task<PredictDto> GetPredict([FromRoute] Guid id) =>
        _predictService.GetPredict(id);

    [HttpGet("statistic")]
    public Task<RentStatisticDto> GetStatistic() =>
        _statisticCollector.GetStatistic();
}
