using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using TodoApi.API;
using TodoApi.Core.DTO;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Repositories;


namespace Tests.UnitTests.Services
{
    // AuthServiceTests.cs
    public class AUTHServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly AUTHService _AUTHService;
        private readonly IConfiguration _config;
        private IUserRepository _userRepo;
        private AppDbContext? context;
        public AUTHServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                {"Jwt:Key", "super-secret-32-character-key"},
                    {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"},
                {"Jwt:ExpiryInMinutes", "60"}
                }).Build();
            _userRepo = new UserRepository(context!);
            _AUTHService = new AUTHService(_mockContext.Object, _config, _userRepo);
        }

        [Fact]
        public async Task Register_ShouldCreateUser()
        {
            // Arrange
            var dto = new UserRegisterDto { Email = "test@test.com", Password = "password" };
            var mockSet = new Mock<DbSet<User>>();
            _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _AUTHService.Register(dto);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<User>(), default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.Equal(dto.Email, result.Email);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsValid()
        {
            // Arrange
            var user = new User { Email = "test@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") };
            var mockSet = new Mock<DbSet<User>>(user);
            _mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var token = await _AUTHService.Login(new UserLoginDto { Email = "test@test.com", Password = "password" });

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}
