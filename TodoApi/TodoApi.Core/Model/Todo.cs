namespace TodoApi.Core.Model
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; } // Внешний ключ
        public User? User { get; set; }
    }


}
