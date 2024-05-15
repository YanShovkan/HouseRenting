namespace HouseRenting.Common.CQRS;

/// <summary>
/// Обработчик запросов.
/// </summary>
/// <typeparam name="T">Модель для запроса</typeparam>
/// <typeparam name="M">Модель для ответа</typeparam>
public interface IQueryHandler<T, M> where T : IQueryDto where M : IQueryDto
{
    /// <summary>
    /// Обработка запроса.
    /// </summary>
    /// <param name="model">Модель данных</param>
    /// <returns></returns>
    public Task<M> HandleAsync(T model);
}

