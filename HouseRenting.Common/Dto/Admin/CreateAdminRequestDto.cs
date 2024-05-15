using HouseRenting.Common.CQRS;

namespace HouseRenting.Common.Dto.Admin;

public record CreateAdminRequestDto() : ICommandDto
{
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}