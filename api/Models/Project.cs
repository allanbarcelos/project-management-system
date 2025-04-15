/* Name: Huynh Tu Anh Chau
GitHub Account: tuanh00
Date: 2025-03-28
Modified by: Gurleen
Modification: Added data validation attributes and custom validation
*/
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    // Added by Gurleen: Project model with data validation attributes
    public class Project
    {
        public int Id { get; set; }

        // Added by Gurleen: Validation for project name
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Project name must be between 3 and 100 characters")]
        public string Name { get; set; } = default!;

        // Added by Gurleen: Validation for description length
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        // Added by Gurleen: Validation for required start date
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        // Added by Gurleen: Custom validation for end date
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime? EndDate { get; set; }

        // Added by Gurleen: Validation for required status
        [Required(ErrorMessage = "Project status is required")]
        public ProjectStatus Status { get; set; }

        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }

    // Added by Gurleen: Custom validation attribute for date comparison
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var currentValue = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance)!;

            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}