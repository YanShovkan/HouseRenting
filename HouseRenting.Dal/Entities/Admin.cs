﻿namespace HouseRenting.Dal.Entities;

public class Admin : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual List<Order> Orders { get; set; } = new List<Order>();
}