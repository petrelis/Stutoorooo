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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Stutooroo.Models;
using static System.Formats.Asn1.AsnWriter;

namespace Stutooroo.Pages.Listings
{
    [Authorize(Roles = "Tutor, Admin")]
    public class CreateModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public CreateModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public IActionResult OnGet()
        {
            ViewData["ExperienceLvlId"] = new SelectList(_context.ExperienceLvl, "Id", "Name");
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Name");
            Console.WriteLine("\n" + _env.WebRootPath + "\n");
            return Page();
        }

        [BindProperty]
        public Listing Listing { get; set; } = default!;
        [BindProperty]
        public IFormFileCollection? ImageFiles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var experienceLvl = new ExperienceLvl();
            experienceLvl.Id = Listing.ExperienceLvlId;
            Listing.ExperienceLvl = await _context.ExperienceLvl.FirstOrDefaultAsync(m => m.Id == experienceLvl.Id);
            var subjectGroup = new SubjectGroup();
            subjectGroup.Id = Listing.SubjectGroupId;
            Listing.SubjectGroup = await _context.SubjectGroups.FirstOrDefaultAsync(m => m.Id == subjectGroup.Id);
            var currentUser = await _userManager.GetUserAsync(User);

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

            // Save listing images
            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                foreach (var file in ImageFiles)
                {
                    var uploadsDirectory = Path.Combine(_env.WebRootPath, "Images");
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(uploadsDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Create a ListingImage and save its path
                    var listingImage = new ListingImage
                    {
                        ListingId = Listing.Id,
                        ImagePath = "/Images/" + fileName // Store the relative path
                    };

                    _context.ListingImages.Add(listingImage);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
