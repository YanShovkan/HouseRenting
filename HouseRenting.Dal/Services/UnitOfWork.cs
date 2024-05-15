using HouseRenting.Dal.Services.Contracts;

namespace HouseRenting.Dal.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    /// <summary>
    /// Создание экземпляра объекта типа <see cref="UnitOfWork"/>
    /// </summary>
    /// <param name="context"></param>
    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void SaveChanges() =>
        _context.SaveChanges();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync() =>
         _context.SaveChangesAsync();
}

