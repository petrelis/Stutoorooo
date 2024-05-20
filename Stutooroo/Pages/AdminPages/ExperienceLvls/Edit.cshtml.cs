using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.ExperienceLvls
{
    public class EditModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public EditModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExperienceLvl ExperienceLvl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ExperienceLvl == null)
            {
                return NotFound();
            }

            var experiencelvl =  await _context.ExperienceLvl.FirstOrDefaultAsync(m => m.Id == id);
            if (experiencelvl == null)
            {
                return NotFound();
            }
            ExperienceLvl = experiencelvl;
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

            _context.Attach(ExperienceLvl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienceLvlExists(ExperienceLvl.Id))
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

        private bool ExperienceLvlExists(int id)
        {
          return (_context.ExperienceLvl?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
