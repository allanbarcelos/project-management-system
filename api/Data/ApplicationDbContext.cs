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

        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<TaskItem> Tasks { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Many to many relationship between User and Projects
            builder.Entity<User>()
            .HasMany(u=> u.Projects)
            .WithMany(p => p.Users)
            .UsingEntity<UserProject>(
                p => p.HasOne(prop => prop.Project).WithMany().HasForeignKey(prop => prop.ProjectId),
                p => p.HasOne(prop => prop.User).WithMany().HasForeignKey(prop => prop.UserId),
                p => {
                    p.HasKey(prop => new {prop.ProjectId, prop.UserId});
                }
            );

        } 

    }
}