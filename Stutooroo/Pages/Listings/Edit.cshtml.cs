using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Listings
{
    [Authorize(Roles = "Tutor, Admin")]
    public class EditModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager)
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

            var listing =  await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            Listing = listing;

            var currentUser = await _userManager.GetUserAsync(User);

            if (Listing.PostedByUserId != currentUser.Id)
                return Unauthorized();

           ViewData["ExperienceLvlId"] = new SelectList(_context.ExperienceLvl, "Id", "Name");
           ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Listing.PostedAtDateTime = DateTime.Now;
            Listing.PostedByUser = currentUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(Listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListingExists(int id)
        {
          return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
