using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.SubjectGroups
{
    public class EditModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public EditModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubjectGroup SubjectGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubjectGroups == null)
            {
                return NotFound();
            }

            var subjectgroup =  await _context.SubjectGroups.FirstOrDefaultAsync(m => m.Id == id);
            if (subjectgroup == null)
            {
                return NotFound();
            }
            SubjectGroup = subjectgroup;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SubjectGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectGroupExists(SubjectGroup.Id))
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

        private bool SubjectGroupExists(int id)
        {
          return (_context.SubjectGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
