/* Name: Queen Sarah Anumu Bih
GitHub Account: Queen-Sarah21
Date: 2025-04-04
*/
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;

        public int TaskId { get; set; }
        public TaskItem Task {get; set;} = default!;

        public string UserId {get; set; } = default!;
        public IdentityUser User { get; set; } = default!;

    }
}