using TodoApi.Core.DTO;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;

namespace TodoApi.Core.Service
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todo> GetTodoByIdAsync(Guid id, Guid userId)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null || todo.UserId != userId)
                throw new KeyNotFoundException("Todo not found.");
            return todo;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(Guid userId, TodoFilter filter, int page, int pageSize)
        {
            return await _todoRepository.GetTodosAsync(userId, filter, page, pageSize);
        }

        public async Task<Todo> CreateTodoAsync(TodoCreateDto dto, Guid userId)
        {
            var todo = new Todo
            {
                Title = dto.Title,
                UserId = userId
            };
            return await _todoRepository.AddAsync(todo);
        }

        public async Task<Todo> UpdateTodoAsync(Guid id, TodoUpdateDto dto, Guid userId)
        {
            var todo = await GetTodoByIdAsync(id, userId);
            todo.Title = dto.Title;
            todo.IsCompleted = dto.IsCompleted;
            return await _todoRepository.UpdateAsync(todo);
        }

        public async Task DeleteTodoAsync(Guid id, Guid userId)
        {
            var todo = await GetTodoByIdAsync(id, userId);
            await _todoRepository.DeleteAsync(todo.Id);
        }
    }
}
