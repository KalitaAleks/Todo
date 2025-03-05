using TodoApi.Core.DTO;
using TodoApi.Core.Model;

namespace TodoApi.Core.Inerface
{
    public interface ITodoService
    {
        Task<Todo> GetTodoByIdAsync(Guid id, Guid userId);
        Task<IEnumerable<Todo>> GetTodosAsync(Guid userId, TodoFilter filter, int page, int pageSize);
        Task<Todo> CreateTodoAsync(TodoCreateDto dto, Guid userId);
        Task<Todo> UpdateTodoAsync(Guid id, TodoUpdateDto dto, Guid userId);
        Task DeleteTodoAsync(Guid id, Guid userId);
    }
}
