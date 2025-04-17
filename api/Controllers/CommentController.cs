using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using System.Security.Claims;
 
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
 
        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
 
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetCommentsByTask(int taskId)
        {
            var comments = await _context.Comments
                .Where(c => c.TaskId == taskId)
                .ToListAsync();
 
            return Ok(comments);
        }
 
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
 
            comment.UserId = userId;
            comment.CreatedAt = DateTime.UtcNow;
 
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();
 
            return Ok(comment);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment updated)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();
 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || comment.UserId != userId)
                return Forbid();
 
            comment.Content = updated.Content;
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();
 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || comment.UserId != userId)
                return Forbid();
 
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
    }
}