using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stutooroo.Models;

namespace Stutooroo.Pages.ExperienceLvls
{
    public class CreateModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public CreateModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExperienceLvl ExperienceLvl { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ExperienceLvl == null || ExperienceLvl == null)
            {
                return Page();
            }

            _context.ExperienceLvl.Add(ExperienceLvl);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
