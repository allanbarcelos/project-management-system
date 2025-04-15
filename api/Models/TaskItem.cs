/* Name: Hazel Clarisse Connolly
GitHub Account: hazelclarisse
Date: 2025-04-04
Modified by: Gurleen
Modification: Added data validation attributes and custom validation
*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public enum TaskItemStatus
    {
        ToDo,
        InProgress,
        Done
    }

    // Added by Gurleen: TaskItem model with data validation attributes
    public class TaskItem
    {
        public int Id { get; set; }

        // Added by Gurleen: Validation for task title
        [Required(ErrorMessage = "Task title is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Task title must be between 3 and 200 characters")]
        public string Title { get; set; } = default!;

        // Added by Gurleen: Validation for task description
        [Required(ErrorMessage = "Task description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = default!;

        // Added by Gurleen: Custom validation for future date
        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime? DueDate { get; set; }

        // Added by Gurleen: Validation for required status
        [Required(ErrorMessage = "Task status is required")]
        public TaskItemStatus Status { get; set; }

        // Added by Gurleen: Validation for required project relationship
        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; } = default!;
    }

    // Added by Gurleen: Custom validation attribute for future date check
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            DateTime dateTime = (DateTime)value;

            if (dateTime.Date < DateTime.Today)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}