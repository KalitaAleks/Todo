using Moq;
using TodoApi.Core.DTO;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;
using TodoApi.Core.Service;

namespace Tests.UnitTests.Services
{
    // TodoServiceTests.cs
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _todoService = new TodoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetTodoById_ShouldThrow_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Todo)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _todoService.GetTodoByIdAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task CreateTodo_ShouldSetUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = new TodoCreateDto { Title = "Test" };

            // Act
            var result = await _todoService.CreateTodoAsync(dto, userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Todo>()), Times.Once);
        }
    }
}
