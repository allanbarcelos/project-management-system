namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using API.Data;

    [Route("/[controller]")] 
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
       

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getTaskDetails")]
        [Authorize(Policy = "RequireUserRole")]
        public IActionResult GetTaskDetails(int taskId)
        {
            var taskItem = _context.Tasks.Where(x=> x.Id == taskId).FirstOrDefault();
            return Ok(new { taskItem });
        }
    }
}