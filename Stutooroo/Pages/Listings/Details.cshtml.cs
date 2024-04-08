using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Listings
{
    public class DetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public DetailsModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

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
            }
            return Page();
        }
    }
}
