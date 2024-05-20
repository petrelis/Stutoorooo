using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stutooroo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? ImagePath { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Phone]
        [DisplayName("Phone Number")]
        public string? PhoneNo { get; set; }
    }
}
