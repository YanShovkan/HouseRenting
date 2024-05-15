using HouseRenting.Common.CQRS;
using HouseRenting.Common.Dto.Admin;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Mvc;

namespace HouseRenting.Api.Controllers;

[ApiController]
[Route("admins")]
public class AdminsController : Controller, IAdminsClient
{
    private readonly IQueryHandler<GetAdminRequestDto, GetAdminResponseDto> _getAdminQueryHandler;
    private readonly ICommandHandler<CreateAdminRequestDto> _createAdminCommandHandler;
    private readonly ICommandHandler<UpdateAdminRequestDto> _updateAdminCommandHandler;
    private readonly ICommandHandler<DeleteAdminRequestDto> _deleteAdminCommandHandler;

    public AdminsController(IQueryHandler<GetAdminRequestDto, GetAdminResponseDto> getAdminQueryHandler,
                           ICommandHandler<CreateAdminRequestDto> createAdminCommandHandler,
                           ICommandHandler<UpdateAdminRequestDto> updateAdminCommandHandler,
                           ICommandHandler<DeleteAdminRequestDto> deleteAdminCommandHandler)
    {
        _getAdminQueryHandler = getAdminQueryHandler;
        _createAdminCommandHandler = createAdminCommandHandler;
        _updateAdminCommandHandler = updateAdminCommandHandler;
        _deleteAdminCommandHandler = deleteAdminCommandHandler;
    }
    
    [HttpGet]
    public Task<GetAdminResponseDto> GetAdmin([FromQuery] Guid? id = default, [FromQuery] string? login = default) =>
        _getAdminQueryHandler.HandleAsync(new(id, login));

    [HttpPost]
    public Task CreateAdmin([FromBody] CreateAdminRequestDto model) =>
        _createAdminCommandHandler.HandleAsync(model);

    [HttpPut]
    public Task UpdateAdmin([FromBody] UpdateAdminRequestDto model) =>
        _updateAdminCommandHandler.HandleAsync(model);

    [HttpDelete("{id}")]
    public Task DeleteAdmin([FromRoute] Guid id) =>
         _deleteAdminCommandHandler.HandleAsync(new(id));
}