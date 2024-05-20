using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Comments
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public IndexModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

        public IList<Comment> Comments { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Comments != null)
            {
                Comments = await _context.Comments.ToListAsync();
            }
        }
    }
}
