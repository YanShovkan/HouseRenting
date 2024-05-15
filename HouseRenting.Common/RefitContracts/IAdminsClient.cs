using HouseRenting.Common.Dto.Admin;
using Refit;

namespace HouseRenting.Common.RefitContracts;

public interface IAdminsClient
{
    [Get("/admins")]
    public Task<GetAdminResponseDto> GetAdmin([Query] Guid? id = default, [Query] string? login = default);

    [Post("/admins")]
    public Task CreateAdmin([Body] CreateAdminRequestDto model);

    [Put("/admins")]
    public Task UpdateAdmin([Body] UpdateAdminRequestDto model);

    [Delete("/admins/{id}")]
    public Task DeleteAdmin(Guid id);
}
