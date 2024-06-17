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
    public class TutorUserDetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TutorUserDetailsModel(StutoorooContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser? AppUser { get; set; }
        public ApplicationUser? CurrentUser { get; set; }
        public IList<Listing>? Listings { get; set; } = default!;
        public IList<ListingImage> Images { get; set; } = default!;
        public Listings.IndexModel IndexPartialModel { get; set; } = default!;

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

            Listings = await _context.Listings
                .Include(l => l.ExperienceLvl)
                .Include(l => l.PostedByUser)
                .Include(l => l.SubjectGroup)
                .Where(l => l.PostedByUserId == AppUser.Id)
                .Take(10)
                .ToListAsync();

            if (_context.ListingImages != null)
            {
                Images = await _context.ListingImages
                .ToListAsync();
            }
            
            CurrentUser = await _userManager.GetUserAsync(User);

            IndexPartialModel = new Listings.IndexModel(_context, _userManager);
            IndexPartialModel = IndexPartialModel.InitIndexPartial(Listings, Images, "Tutor's Listings");

            return Page();
        }
    }
}
