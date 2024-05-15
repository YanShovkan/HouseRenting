namespace HouseRenting.Dal.Services.Contracts;

/// <summary>
/// Единый сервис для сохранения результатов работы с БД.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Зафиксировать измененеия в БД.
    /// </summary>
    /// <returns></returns>
    public void SaveChanges();

    /// <summary>
    /// Асинхронно зафиксировать измененеия в БД.
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync();
}

