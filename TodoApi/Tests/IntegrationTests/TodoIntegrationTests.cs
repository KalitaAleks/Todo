// TodoIntegrationTests.cs
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Tests.Data;
using TodoApi.Infrastructure;

namespace Tests.IntegrationTests;

public class TodoIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly AppDbContext _context;

    public TodoIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Заменяем реальную БД на InMemory
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                services.Remove(descriptor!);
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("IntegrationTestDb"));
            });
        });
        _client = _factory.CreateClient();
        _context = _factory.Services.GetRequiredService<AppDbContext>();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreateTodo_ReturnsCreatedTodo()
    {
        // Arrange
        var dto = TestData.TodoCreateDtoFaker.Generate();
        var user = TestData.UserFaker.Generate();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Авторизация (если требуется)
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TestJwtGenerator.Generate(user)}");

        // Act
        var response = await _client.PostAsJsonAsync("/api/todos", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(await _context.Todos.FirstOrDefaultAsync(t => t.Title == dto.Title));
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
