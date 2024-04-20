using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Listings
{
    public class DeleteModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
      public Listing Listing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);

            if (listing == null)
            {
                return NotFound();
            }
            else 
            {
                Listing = listing;

                var currentUser = await _userManager.GetUserAsync(User);

                if (Listing.PostedByUserId != currentUser.Id)
                    return Unauthorized();

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);

            if (listing != null)
            {
                Listing = listing;
                _context.Listings.Remove(Listing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
