using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stutooroo.Models;
using static System.Formats.Asn1.AsnWriter;

namespace Stutooroo.Pages.Listings
{
    [Authorize(Roles = "Tutor, Admin")]
    public class CreateModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(Stutooroo.Models.StutoorooContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ViewData["ExperienceLvlId"] = new SelectList(_context.ExperienceLvl, "Id", "Name");
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Listing Listing { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var experienceLvl = new ExperienceLvl();
            experienceLvl.Id = Listing.ExperienceLvlId;
            var subjectGroup = new SubjectGroup();
            subjectGroup.Id = Listing.SubjectGroupId;
            Listing.PostedByUser = currentUser;
            Listing.PostedByUserId = currentUser.Id;
            Listing.PostedAtDateTime = DateTime.Now;
            if (!ModelState.IsValid || _context.Listings == null || Listing == null)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // Log or handle the error messages
                        Console.WriteLine(error.ErrorMessage);
                        // You can also inspect the exception if there's one
                        Console.WriteLine(error.Exception);
                    }
                }

                return Page();
            }
          
            _context.Listings.Add(Listing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
