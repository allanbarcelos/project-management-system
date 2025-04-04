/* Name: Huynh Tu Anh Chau
GitHub Account: tuanh00
Date: 2025-03-28
*/
namespace API.Models
{
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
    }
}