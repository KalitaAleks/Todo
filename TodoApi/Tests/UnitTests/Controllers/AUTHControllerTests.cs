using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.API.Controllers;
using TodoApi.Core.DTO;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;

namespace Tests.UnitTests.Controllers
{
    // AUTHControllerTests.cs
    public class AUTHControllerTests
    {
        private readonly Mock<IAuthService> _mockAUTHService;
        private readonly AUTHController _controller;

        public AUTHControllerTests()
        {
            _mockAUTHService = new Mock<IAuthService>();
            _controller = new AUTHController(_mockAUTHService.Object);
        }

        [Fact]
        public async Task Register_ShouldReturn201_WhenSuccess()
        {
            // Arrange
            var dto = new UserRegisterDto { Email = "test@test.com", Password = "password" };
            _mockAUTHService.Setup(s => s.Register(dto)).ReturnsAsync(new User { Email = dto.Email });

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtResult.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturn401_WhenInvalidCredentials()
        {
            // Arrange
            _mockAUTHService.Setup(s => s.Login(It.IsAny<UserLoginDto>()))
                .ThrowsAsync(new UnauthorizedAccessException());

            // Act
            var result = await _controller.Login(new UserLoginDto());

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
