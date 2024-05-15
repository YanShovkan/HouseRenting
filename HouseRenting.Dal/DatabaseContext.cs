using HouseRenting.Dal.Entities;
using HouseRenting.Dal.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Dal;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdminEntityConfiguration).Assembly);
    }
}
