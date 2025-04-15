using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs
{
    public class CreateTaskItemDTO
    {
        [Required(ErrorMessage = "Task title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Task title must be between 3 and 200 characters")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Task description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = default!;

        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        public TaskItemStatus Status { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectId { get; set; }
    }

    public class UpdateTaskItemDTO : CreateTaskItemDTO
    {
        [Required(ErrorMessage = "Task ID is required")]
        public int Id { get; set; }
    }

    public class TaskItemResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime? DueDate { get; set; }
        public TaskItemStatus Status { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = default!;
    }
} 