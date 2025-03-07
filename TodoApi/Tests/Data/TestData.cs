// TestData.cs
using Bogus;
using TodoApi.Core.DTO;
using TodoApi.Core.Model;

namespace Tests.Data;

public static class TestData
{
    public static Faker<User> UserFaker => new Faker<User>()
        .RuleFor(u => u.Id, _ => Guid.NewGuid())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.PasswordHash, _ => BCrypt.Net.BCrypt.HashPassword("password"));

    public static Faker<Todo> TodoFaker => new Faker<Todo>()
        .RuleFor(t => t.Id, _ => Guid.NewGuid())
        .RuleFor(t => t.Title, f => f.Lorem.Sentence())
        .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
        .RuleFor(t => t.CreatedAt, f => f.Date.Past());

    public static Faker<TodoCreateDto> TodoCreateDtoFaker => new Faker<TodoCreateDto>()
        .RuleFor(t => t.Title, f => f.Lorem.Sentence());
}

// Генератор JWT для интеграционных тестов
public static class TestJwtGenerator
{
    public static string Generate(User user)
    {
        // Реализация аналогична вашему AuthService
        // ...
        return "fake-jwt-token";
    }
}