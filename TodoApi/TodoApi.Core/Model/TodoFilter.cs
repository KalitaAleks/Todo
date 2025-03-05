namespace TodoApi.Core.Model
{
    public class TodoFilter
    {
        public bool? IsCompleted { get; set; }     // Фильтр по статусу
        public DateTime? CreatedAfter { get; set; } // Фильтр по дате создания
    }
}
