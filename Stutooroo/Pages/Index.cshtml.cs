using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stutooroo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Stutooroo.Models.StutoorooContext _context;

        public IndexModel(ILogger<IndexModel> logger, Models.StutoorooContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int ListingCount { get; set; }

        public void OnGet()
        {
            ListingCount = _context.Listings.Count();
        }
    }
}