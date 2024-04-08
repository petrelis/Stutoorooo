using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.SubjectGroups
{
    public class DetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public DetailsModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

      public SubjectGroup SubjectGroup { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubjectGroups == null)
            {
                return NotFound();
            }

            var subjectgroup = await _context.SubjectGroups.FirstOrDefaultAsync(m => m.Id == id);
            if (subjectgroup == null)
            {
                return NotFound();
            }
            else 
            {
                SubjectGroup = subjectgroup;
            }
            return Page();
        }
    }
}
