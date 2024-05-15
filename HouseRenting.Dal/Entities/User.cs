namespace HouseRenting.Dal.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual List<Request> Requests { get; set; } = new();

    public virtual List<Advert> Adverts { get; set; } = new();
}
