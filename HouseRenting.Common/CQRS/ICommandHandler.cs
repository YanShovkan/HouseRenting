namespace HouseRenting.Common.CQRS;

/// <summary>
/// Обработчик команд.
/// </summary>
/// <typeparam name="T">Модель для запроса</typeparam>
public interface ICommandHandler<T> where T : class, ICommandDto
{
    /// <summary>
    /// Обработка команды.
    /// </summary>
    /// <param name="model">Модель данных</param>
    public Task HandleAsync(T model);
}

