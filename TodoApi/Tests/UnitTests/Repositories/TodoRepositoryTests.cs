// TodoRepositoryTests.cs
using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Model;
using TodoApi.Core.Service;
using TodoApi.Infrastructure;
using Xunit;
using Tests.Data;
using TodoApi.Infrastructure.Repositories;


namespace Tests.UnitTests.Repositories;

public class TodoRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public TodoRepositoryTests()
    {
        // Используем InMemory Database для изоляции тестов
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TodoRepoTestDb")
            .Options;
    }

    [Fact]
    public async Task AddAsync_ShouldAddTodoToDatabase()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new TodoRepository(context);
        var todo = TestData.TodoFaker.Generate();

        // Act
        var result = await repository.AddAsync(todo);

        // Assert
        Assert.NotNull(await context.Todos.FindAsync(todo.Id));
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyExistingTodo()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new TodoRepository(context);
        var todo = TestData.TodoFaker.Generate();
        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        todo.Title = "Updated Title";

        // Act
        var result = await repository.UpdateAsync(todo);

        // Assert
        Assert.Equal("Updated Title", (await context.Todos.FindAsync(todo.Id))?.Title);
    }
}