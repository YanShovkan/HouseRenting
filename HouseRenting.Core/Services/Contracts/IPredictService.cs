using HouseRenting.Common.Dto.Services;

namespace HouseRenting.Core.Services.Contracts;

public interface IPredictService
{
    public Task<PredictDto> GetPredict(Guid Id);
}
