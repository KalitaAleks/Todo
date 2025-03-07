using Microsoft.EntityFrameworkCore;
using System;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;
using TodoApi.Infrastructure;

namespace TodoApi.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Todo?> GetByIdAsync(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IEnumerable<Todo>> GetTodosAsync(Guid userId, TodoFilter filter, int page, int pageSize)
    {
        var query = _context.Todos.Where(t => t.UserId == userId);

        // Фильтрация по статусу
        if (filter.IsCompleted.HasValue)
            query = query.Where(t => t.IsCompleted == filter.IsCompleted);

        // Фильтрация по дате создания
        if (filter.CreatedAfter.HasValue)
            query = query.Where(t => t.CreatedAt >= filter.CreatedAfter);

        // Пагинация
        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> UpdateAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task DeleteAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
