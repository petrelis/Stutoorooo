﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.ExperienceLvls
{
    public class DetailsModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        public DetailsModel(Stutooroo.Models.StutoorooContext context)
        {
            _context = context;
        }

      public ExperienceLvl ExperienceLvl { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ExperienceLvl == null)
            {
                return NotFound();
            }

            var experiencelvl = await _context.ExperienceLvl.FirstOrDefaultAsync(m => m.Id == id);
            if (experiencelvl == null)
            {
                return NotFound();
            }
            else 
            {
                ExperienceLvl = experiencelvl;
            }
            return Page();
        }
    }
}
