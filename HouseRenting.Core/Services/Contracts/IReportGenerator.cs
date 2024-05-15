using HouseRenting.Common.Dto.Services;

namespace HouseRenting.Core.Services.Contracts;

public interface IReportGenerator
{
    Task<byte[]> CreateReportAsync(GenerateReportDto model, CancellationToken cancellationToken = default);
}
