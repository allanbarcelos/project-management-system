namespace API.Models{
    using Microsoft.AspNetCore.Identity;
    public class User : IdentityUser
    {
        public ICollection<Project> Projects { get; set; }
    }
}