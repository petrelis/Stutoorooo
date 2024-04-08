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
    public class IndexModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public IndexModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

        public IList<Listing> Listing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Listings != null)
            {
                Listing = await _context.Listings
                .Include(l => l.ExperienceLvl)
                .Include(l => l.PostedByUser)
                .Include(l => l.SubjectGroup).ToListAsync();
            }
        }
    }
}
