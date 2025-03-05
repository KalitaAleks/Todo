using System.ComponentModel.DataAnnotations;

namespace TodoApi.Core.DTO
{
    public class TodoUpdateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
