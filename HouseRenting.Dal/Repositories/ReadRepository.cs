using HouseRenting.Dal.Entities;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Dal.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Dal.Repositories;

/// <summary>
/// <inheritdoc/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class ReadRepository<T> : IReadRepository<T> where T : class, IEntity
{
    protected virtual IQueryable<T> Query { get; set; }

    /// <summary>
    /// Создание экземпляра объекта типа <see cref="ReadRepository{T}"/>
    /// </summary>
    /// <param name="context"></param>
    protected ReadRepository(DatabaseContext context)
    {
        Query = context.Set<T>();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Query.ToArrayAsync(cancellationToken);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"><inheritdoc/></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Query.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="specification"><inheritdoc/></param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<T>> GetBySpecification(Specification<T> specification, CancellationToken cancellationToken = default)
        => await Query.Where(specification.Predicate).ToArrayAsync(cancellationToken);
}

