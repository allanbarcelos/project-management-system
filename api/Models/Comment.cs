
using System;

namespace API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = default!;
        public int TaskId { get; set; }
        public TaskItem TaskItem { get; set; } = default!;
    }
}
