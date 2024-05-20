using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Users
{
    public class CustomerUserDetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public CustomerUserDetailsModel(StutoorooContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public ApplicationUser? AppUser { get; set; }
        public ApplicationUser? CurrentUser { get; set; }
        public int? Age { get; set; }
        public IList<Listing> Listings { get; set; } = default!;
        public IList<ListingImage> Images { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? username)
        {
            if (username == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            AppUser = user;

            CurrentUser = await _userManager.GetUserAsync(User);

            var favoriteListings = _context.FavoriteListings
                .Where(fl => fl.UserId == AppUser.Id)
                .ToListAsync();

            var listings = new List<Listing>();
            foreach(var f in favoriteListings.Result)
            {
                var listing = _context.Listings
                    .Include(l => l.ExperienceLvl)
                    .Include(l => l.PostedByUser)
                    .Include(l => l.SubjectGroup)
                    .Where(l => l.Id == f.ListingId)
                    .FirstOrDefault();
                if (listing != null)
                    listings.Add(listing);
            }

            Listings = listings.ToList();

            if (_context.ListingImages != null)
            {
                Images = await _context.ListingImages
                .ToListAsync();
            }

            if (AppUser.DateOfBirth.HasValue)
                Age = (int)(DateTime.Now - AppUser.DateOfBirth).Value.TotalDays / 365;

            return Page();
        }
    }
}
