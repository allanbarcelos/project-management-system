/* Name: Hazel Clarisse Connolly
GitHub Account: hazelclarisse
Date: 2025-04-04
*/
namespace API.Models
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime? DueDate { get; set; }
        public TaskStatus Status {get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = default!;

    }
}