// DatabaseFixture.cs
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;


namespace Tests.IntegrationTests;

public class DatabaseFixture : IDisposable
{
    public AppDbContext Context { get; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("IntegrationTestDb")
            .Options;
        Context = new AppDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}