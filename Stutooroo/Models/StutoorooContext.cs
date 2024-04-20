using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Stutooroo.Models
{
    public class StutoorooContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentImage> CommentImages { get; set; }
        public DbSet<ExperienceLvl> ExperienceLvl { get; set; }
        public DbSet<FavoriteListing> FavoriteListings { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }

        public StutoorooContext(DbContextOptions<StutoorooContext> options) : base(options)
        {
        }
    }
}
