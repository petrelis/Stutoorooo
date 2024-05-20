using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Listings
{
    [Authorize(Roles = "Tutor, Admin")]
    public class EditModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public EditModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        [BindProperty]
        public Listing Listing { get; set; } = default!;
        [BindProperty]
        public IFormFileCollection? ImageFiles { get; set; }
        [BindProperty]
        public List<ListingImage> ListingImages { get; set; }

        private string postedByUserId { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing =  await _context.Listings
                .Include(l => l.ExperienceLvl)
                .Include(l => l.PostedByUser)
                .Include(l => l.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (listing == null)
            {
                return NotFound();
            }
            Listing = listing;

            ListingImages = _context.ListingImages.Where(l => l.ListingId == id).ToList();

            var currentUser = await _userManager.GetUserAsync(User);

            if (Listing.PostedByUserId != currentUser.Id && !User.IsInRole("Admin"))
                return Unauthorized();

           ViewData["ExperienceLvlId"] = new SelectList(_context.ExperienceLvl, "Id", "Name");
           ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            Listing.PostedAtDateTime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(Listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


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

        private bool ListingExists(int id)
        {
          return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
