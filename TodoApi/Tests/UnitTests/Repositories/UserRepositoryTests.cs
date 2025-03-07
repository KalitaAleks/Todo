using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Model;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Repositories;

namespace Tests.UnitTests.Repositories;

public class UserRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public UserRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserRepoTestDb")
            .Options;
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new UserRepository(context);
        var user = new User { Email = "test@test.com", PasswordHash = "hash" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByEmailAsync("test@test.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Email, result.Email);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveUser()
    {
        // Arrange
        using var context = new AppDbContext(_options);
        var repository = new UserRepository(context);
        var user = new User { Email = "test@test.com", PasswordHash = "hash" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(user.Id);

        // Assert
        Assert.Null(await context.Users.FindAsync(user.Id));
    }
}