using API.Models;

namespace API.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<TaskItem> Tasks { get; set; } = default!;
    }
}