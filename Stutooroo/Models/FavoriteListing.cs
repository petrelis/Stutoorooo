using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stutooroo.Models
{
    public class FavoriteListing
    {
        public int Id { get; set; }
        public Listing Listing { get; set; }
        [ForeignKey("Listing")]
        public int ListingId { get; set; }
        [ForeignKey("FavoritedByUser")]
        public string UserId { get; set; }
        public virtual IdentityUser FavoritedByUser { get; set; }
    }
}
