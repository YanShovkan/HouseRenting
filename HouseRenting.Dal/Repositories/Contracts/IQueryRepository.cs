using HouseRenting.Dal.Entities;

namespace HouseRenting.Dal.Repositories.Contracts;

/// <summary>
/// Репозиторий для получения данных из БД.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQueryRepository<T> : IReadRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Получить Queryable для сущности из БД.
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> GetQuery();
}

