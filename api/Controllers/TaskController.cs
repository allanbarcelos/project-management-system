using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemResponseDTO>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Select(t => new TaskItemResponseDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    ProjectId = t.ProjectId,
                    ProjectName = t.Project.Name
                })
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemResponseDTO>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }

            var taskDto = new TaskItemResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                ProjectId = task.ProjectId,
                ProjectName = task.Project.Name
            };

            return Ok(taskDto);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemResponseDTO>> CreateTask(CreateTaskItemDTO taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the project exists
            var project = await _context.Projects.FindAsync(taskDto.ProjectId);
            if (project == null)
            {
                ModelState.AddModelError("ProjectId", "The specified project does not exist");
                return BadRequest(ModelState);
            }

            // Validate due date is after project start date
            if (taskDto.DueDate.HasValue && taskDto.DueDate.Value < project.StartDate)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be before project start date");
                return BadRequest(ModelState);
            }

            // Validate due date is before project end date if it exists
            if (taskDto.DueDate.HasValue && project.EndDate.HasValue && taskDto.DueDate.Value > project.EndDate.Value)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be after project end date");
                return BadRequest(ModelState);
            }

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = taskDto.Status,
                ProjectId = taskDto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var response = new TaskItemResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                ProjectId = task.ProjectId,
                ProjectName = project.Name
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskItemDTO taskDto)
        {
            if (id != taskDto.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }

            // Validate that the project exists
            var project = await _context.Projects.FindAsync(taskDto.ProjectId);
            if (project == null)
            {
                ModelState.AddModelError("ProjectId", "The specified project does not exist");
                return BadRequest(ModelState);
            }

            // Validate due date is after project start date
            if (taskDto.DueDate.HasValue && taskDto.DueDate.Value < project.StartDate)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be before project start date");
                return BadRequest(ModelState);
            }

            // Validate due date is before project end date if it exists
            if (taskDto.DueDate.HasValue && project.EndDate.HasValue && taskDto.DueDate.Value > project.EndDate.Value)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be after project end date");
                return BadRequest(ModelState);
            }

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate;
            task.Status = taskDto.Status;
            task.ProjectId = taskDto.ProjectId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound(new { message = $"Task with ID {id} not found" });
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
} 