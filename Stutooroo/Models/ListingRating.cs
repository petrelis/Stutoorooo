using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class ListingRating
    {
        public int Id { get; set; }
        public Listing Listing { get; set; }
        [ForeignKey("Listing")]
        public int ListingId { get; set; }
        public float Rating { get; set; }
        [ForeignKey("PostedByUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }

    }
}
