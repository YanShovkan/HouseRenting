using HouseRenting.Common.Dto.User;
using Refit;

namespace HouseRenting.Common.RefitContracts;

public interface IUsersClient
{
    [Get("/users/")]
    public Task<GetUserResponseDto> GetUser([Query] Guid? id = default, [Query] string? login = default);

    [Post("/users/")]
    public Task CreateUser([Body] CreateUserRequestDto model);

    [Put("/users/")]
    public Task UpdateUser([Body] UpdateUserRequestDto model);

    [Delete("/users/{id}")]
    public Task DeleteUser(Guid id);
}
