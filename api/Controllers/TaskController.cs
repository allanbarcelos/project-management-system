using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Get the data from database
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] API.Models.TaskStatus newStatus)
        {
            // Look for the task using the ID
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            task.Status = newStatus;

            // Save change
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task status updated successfully." });
        }
    }
}
