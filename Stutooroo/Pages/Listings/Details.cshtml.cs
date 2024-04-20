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

namespace Stutooroo.Pages.Listings
{
    public class DetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Listing Listing { get; set; } = default!;
        public ExperienceLvl ExperienceLvl { get; set; } = default!;
        public SubjectGroup SubjectGroup { get; set; } = default!;
        public ApplicationUser PostedByUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FirstOrDefaultAsync(m => m.Id == id);
            var experienceLvl = await _context.ExperienceLvl.FirstOrDefaultAsync(m => m.Id == listing.ExperienceLvlId);
            var subjectGroup = await _context.SubjectGroups.FirstOrDefaultAsync(m => m.Id == listing.SubjectGroupId);
            var postedByUser = await _userManager.FindByIdAsync(listing.PostedByUserId);
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
            }
            return Page();
        }
    }
}
