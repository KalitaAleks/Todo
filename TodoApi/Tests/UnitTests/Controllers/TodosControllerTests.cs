using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System.Security.Claims;
using TodoApi.API.Controllers;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;

namespace Tests.UnitTests.Controllers
{
    // TodosControllerTests.cs
    public class TodosControllerTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodosController _controller;
        private readonly Guid _userId = Guid.NewGuid();

        public TodosControllerTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, _userId.ToString())
            }))
            };
            _controller = new TodosController(_mockTodoService.Object, new HttpContextAccessor { HttpContext = httpContext });
        }

        [Fact]
        public async Task GetAll_ShouldReturnPaginatedResponse()
        {
            // Arrange
            var mockTodos = new List<Todo> { new Todo { Title = "Test" } };
            _mockTodoService.Setup(s => s.GetTodosAsync(_userId, null, 1, 10))
                .ReturnsAsync(mockTodos);
            TodoFilter filter = new TodoFilter();
            // Act
            var result = await _controller.GetAll(filter, 1,10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<Todo>>(okResult.Value);
        }

        [Fact]
        public async Task Delete_ShouldReturn204_WhenSuccess()
        {
            // Arrange
            _mockTodoService.Setup(s => s.DeleteTodoAsync(It.IsAny<Guid>(), _userId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(Guid.NewGuid());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
