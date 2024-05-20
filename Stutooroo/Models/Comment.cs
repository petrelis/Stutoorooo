using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Listing Listing { get; set; }
        [ForeignKey("Listing")]
        public int ListingId { get; set; }
        [Required]
        public string Text { get; set; }
        [ForeignKey("PostedByUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
    }
}
