using TodoApi.Core.Model;

namespace TodoApi.Core.Inerface
{
    public interface ITodoRepository
    {
        Task<Todo?> GetByIdAsync(Guid id); // Получить задачу по ID
        Task<IEnumerable<Todo>> GetTodosAsync(Guid userId, TodoFilter filter, int page, int pageSize); // Список задач
        Task<Todo> AddAsync(Todo todo);    // Создать задачу
        Task<Todo> UpdateAsync(Todo todo); // Обновить задачу
        Task DeleteAsync(Guid id);         // Удалить задачу
    }
}
