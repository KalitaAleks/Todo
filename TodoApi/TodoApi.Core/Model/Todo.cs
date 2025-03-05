namespace TodoApi.Core.Model
{
    public class Todo
    {
        public Guid Id { get; set; } = Guid.NewGuid();         // Уникальный идентификатор
        public string Title { get; set; } = string.Empty;      // Заголовок задачи
        public bool IsCompleted { get; set; } = false;         // Статус выполнения
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания
        public Guid UserId { get; set; }                       // Внешний ключ пользователя
        public User User { get; set; } = null!;                // Навигационное свойство
    }


}
