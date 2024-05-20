using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;

namespace Stutooroo.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DeleteModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationUser UserToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }
            else
            {
                UserToDelete = userToDelete;
            }

            if (currentUser.Id != userToDelete.Id && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var userToDelete = await _userManager.FindByIdAsync(id);
            if (currentUser.Id != userToDelete.Id && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            if (userToDelete == null)
            {
                return NotFound();
            }

            if (currentUser.Id == userToDelete.Id)
            {
                await _signInManager.SignOutAsync();
            }

            await _userManager.DeleteAsync(userToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage($"/Listings/Index");
        }
    }
}
