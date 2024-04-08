using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class ListingImage
    {
        public int Id { get; set; }
        public Listing Listing { get; set; }
        [ForeignKey("Listing")]
        public int ListingId { get; set; }
        public string ImagePath { get; set; }
    }
}
