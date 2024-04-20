using Microsoft.AspNetCore.Identity;

namespace Stutooroo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? ImagePath { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
