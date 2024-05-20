using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Stutooroo.Models;

namespace Stutooroo.Pages.Listings
{
    public class DetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public DetailsModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public Listing Listing { get; set; } = default!;
        public ExperienceLvl ExperienceLvl { get; set; } = default!;
        public SubjectGroup SubjectGroup { get; set; } = default!;
        public ApplicationUser PostedByUser { get; set; } = default!;
        public List<ListingImage> ListingImages { get; set; }
        public ApplicationUser? CurrentUser { get; set; } = default!;
        public List<FavoriteListing> FavoriteListings { get; set; } = default!;
        public bool ListingIsFav { get; set; }
        public List<Comment> Comments { get; set; } = default!;
        [Display(Name="Comment")]
        [BindProperty]
        [Required]
        public string CommentInputText { get; set; }
        [BindProperty]
        public int ListingIdInput { get; set; }
        public List<ApplicationUser> CommentUsers { get; set; } = default!;
        public float CurrentUserListingRating { get; set; } = 0;

        // Model class to represent the data sent from the client-side
        public class RatingData
        {
            public int RatingValue { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            ListingImages = _context.ListingImages.Where(l => l.ListingId == id).ToList();
            Comments = _context.Comments.Where(c => c.ListingId == id).ToList();

            var users = _context.Users
                .Where(u => Comments.Select(c => c.UserId).Distinct().Contains(u.Id))
                .ToList();

            CommentUsers = users;

            var listing = await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);
            var experienceLvl = await _context.ExperienceLvl.FirstOrDefaultAsync(m => m.Id == listing.ExperienceLvlId);
            var subjectGroup = await _context.SubjectGroups.FirstOrDefaultAsync(m => m.Id == listing.SubjectGroupId);
            var postedByUser = await _userManager.FindByIdAsync(listing.PostedByUserId);
            var currentUser = await _userManager.GetUserAsync(User);
            if (listing == null || experienceLvl == null || subjectGroup == null || postedByUser == null)
            {
                return NotFound();
            }
            else 
            {
                Listing = listing;
                ExperienceLvl = experienceLvl;
                SubjectGroup = subjectGroup;
                PostedByUser = postedByUser;
                if(currentUser != null)
                {
                    CurrentUser = currentUser;
                    ListingIsFav = await _context.FavoriteListings
                        .AnyAsync(fl => fl.ListingId == id && fl.UserId == CurrentUser.Id);
                    var currUserListingRating = _context.ListingRatings
                        .Where(lr => lr.UserId == currentUser.Id 
                        && lr.ListingId == listing.Id).FirstOrDefault();
                    if (currUserListingRating != null)
                    {
                        CurrentUserListingRating = currUserListingRating.Rating;
                    }
                }
			}
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*
            if (CommentInputText.IsNullOrEmpty() || !User.IsInRole("Customer"))
            {
                return RedirectToPage(new { id = ListingIdInput });
            }
            */

            var currUser = await _userManager.GetUserAsync(User);
            if (currUser == null) return Unauthorized();

            var newComment = new Comment
            {
                Listing = _context.Listings.Where(l => l.Id == ListingIdInput).FirstOrDefault(),
                ListingId = ListingIdInput,
                AspNetUser = currUser,
                UserId = currUser.Id,
                Text = CommentInputText
            };

            if (!ModelState.IsValid)
            {
                return RedirectToPage(new { id = ListingIdInput });
            }


            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = ListingIdInput });
        }

        // Handler method to favorite a listing
        public async Task<IActionResult> OnPostFavoriteListingHandlerAsync(int id)
        {
            // Get the current user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return NotFound();

            // Create a new FavoriteListing object
            var favoriteListing = new FavoriteListing
            {
                ListingId = id,
                UserId = userId
            };

            var isFavorited = await _context.FavoriteListings
                .AnyAsync(fl => fl.ListingId == id && fl.UserId == userId);
            ListingIsFav = isFavorited;


            if (!ListingIsFav)
            {
                // Add the new favorite listing to the context
                _context.FavoriteListings.Add(favoriteListing);
            }
            else
            {
                ListingIsFav = false;
                var listingtoRemove = _context.FavoriteListings
                    .Where(fl => fl.ListingId == favoriteListing.ListingId && fl.UserId == userId).ToList();
                _context.FavoriteListings.RemoveRange(listingtoRemove);
            }

            await _context.SaveChangesAsync();

            // Return a success response
            return new JsonResult(new { isFavorited = !ListingIsFav });
        }

        public async Task<IActionResult> OnPostRateListingAsync(int id, int rating)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var listingRating = new ListingRating
            {
                ListingId = id,
                Rating = rating,
                UserId = currentUser.Id
            };

            var isRated = await _context.ListingRatings
                .AnyAsync(lr => lr.ListingId == listingRating.ListingId && lr.UserId == listingRating.UserId);

            if(!isRated)
            {
                await _context.ListingRatings.AddAsync(listingRating);
                await _context.SaveChangesAsync();
                var newRating = _context.ListingRatings.Sum(lr => lr.Rating) / _context.ListingRatings.Count();
                // Update the listing's rating
                listing.Rating = newRating;

            }
            else
            {
                var existingRating = _context.ListingRatings
                    .Where(lr => lr.ListingId == listingRating.ListingId && lr.UserId == listingRating.UserId).FirstOrDefault();

                if(existingRating.Rating != rating)
                {
                    _context.ListingRatings.Remove(existingRating);
                    await _context.ListingRatings.AddAsync(listingRating);
                    await _context.SaveChangesAsync();
                    var newRating = _context.ListingRatings
                        .Where(lr => lr.ListingId == id)
                        .Sum(lr => lr.Rating) / 
                        _context.ListingRatings
                        .Where(lr => lr.ListingId == id)
                        .Count();
                    // Update the listing's rating
                    listing.Rating = newRating;
                }
            }

            await _context.SaveChangesAsync();

            // Return a success response
            return new OkResult();
        }



    }
}
