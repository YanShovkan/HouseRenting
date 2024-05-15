using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.User;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller, IUsersClient
{
    private readonly IQueryHandler<GetUserRequestDto, GetUserResponseDto> _getUserQueryHandler;
    private readonly ICommandHandler<CreateUserRequestDto> _createUserCommandHandler;
    private readonly ICommandHandler<UpdateUserRequestDto> _updateUserCommandHandler;
    private readonly ICommandHandler<DeleteUserRequestDto> _deleteUserCommandHandler;

    public UsersController(IQueryHandler<GetUserRequestDto, GetUserResponseDto> getUserQueryHandler,
                           ICommandHandler<CreateUserRequestDto> createUserCommandHandler,
                           ICommandHandler<UpdateUserRequestDto> updateUserCommandHandler,
                           ICommandHandler<DeleteUserRequestDto> deleteUserCommandHandler)
    {
        _getUserQueryHandler = getUserQueryHandler;
        _createUserCommandHandler = createUserCommandHandler;
        _updateUserCommandHandler = updateUserCommandHandler;
        _deleteUserCommandHandler = deleteUserCommandHandler;
    }

    [HttpGet]
    public Task<GetUserResponseDto> GetUser([FromQuery] Guid? id = default, [FromQuery] string? login = default) =>
        _getUserQueryHandler.HandleAsync(new(id, login));

    [HttpPost]
    public Task CreateUser([FromBody] CreateUserRequestDto model) =>
        _createUserCommandHandler.HandleAsync(model);

    [HttpPut]
    public Task UpdateUser([FromBody] UpdateUserRequestDto model) =>
        _updateUserCommandHandler.HandleAsync(model);

    [HttpDelete("{id}")]
    public Task DeleteUser([FromRoute] Guid id) =>
         _deleteUserCommandHandler.HandleAsync(new(id));

}
