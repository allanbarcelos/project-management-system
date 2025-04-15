using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTOs
{
    public class CreateProjectDTO
    {
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Project name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Project status is required")]
        public ProjectStatus Status { get; set; }
    }

    public class UpdateProjectDTO : CreateProjectDTO
    {
        [Required(ErrorMessage = "Project ID is required")]
        public int Id { get; set; }
    }

    public class ProjectResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public ICollection<TaskItemResponseDTO> Tasks { get; set; }
    }
} 