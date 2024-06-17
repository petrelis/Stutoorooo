using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Stutooroo.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Stutooroo.Pages.Listings
{
    public class IndexModel : PageModel
    {
        private readonly Stutooroo.Models.StutoorooContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(Stutooroo.Models.StutoorooContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IndexModel InitIndexPartial(IList<Listing> listings, IList<ListingImage> images, string partialTitle)
        {
            IndexModel IndexPartialModel = new IndexModel(_context, _userManager);
            if(User != null)
            {
                IndexPartialModel.UserId = _userManager.GetUserId(User);
            }
            else
            {
                IndexPartialModel.UserId = "";
            }
            IndexPartialModel.Listings = listings;
            IndexPartialModel.Images = images;
            IndexPartialModel.PartialTitle = partialTitle;

            return IndexPartialModel;
        }

        public IList<Listing> Listings { get; set; } = default!;
        public IList<ListingImage> Images { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public ApplicationUser CurrentUser { get; set; } = default!;

        public Pager Pager { get; set; }

        [BindProperty(SupportsGet = true)]
        public ListingFilterModel ListingFilter { get; set; }
        public string PartialTitle { get; set; } = default!;

        public async Task OnGetAsync(int pg=1)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                UserId = null;
            else
            {
                UserId = currentUser.Id;
                CurrentUser = currentUser;
            }

            var ExperienceSLI = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Select an experience level" }
            };

            foreach (var level in _context.ExperienceLvl)
            {
                ExperienceSLI.Add(new SelectListItem { Value = level.Id.ToString(), Text = level.Name });
            }

            var SubjectGroupSLI = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Select a Subject Group" }
            };

            foreach (var level in _context.SubjectGroups)
            {
                SubjectGroupSLI.Add(new SelectListItem { Value = level.Id.ToString(), Text = level.Name });
            }

            ViewData["ExperienceLvlId"] = ExperienceSLI;
            ViewData["SubjectGroupId"] = SubjectGroupSLI;

            ListingFilter ??= new ListingFilterModel();
            ListingFilter = ListingFilterModel.FillNullValues(ListingFilter);

            if (_context.Listings != null)
            {
                var query = _context.Listings
                    .Include(l => l.ExperienceLvl)
                    .Include(l => l.PostedByUser)
                    .Include(l => l.SubjectGroup)
                    .OrderByDescending(l => l.PostedAtDateTime)
                    .Where(l => l.PostedAtDateTime > ListingFilter.DateFilterStart &&
                                l.HourlyRate < ListingFilter.HourlyRateFilter &&
                                (ListingFilter.RatingFilter == null || l.Rating >= ListingFilter.RatingFilter));

                // Apply additional filtering based on search string
                if (!string.IsNullOrEmpty(ListingFilter.SearchString))
                {
                    query = query.Where(l => EF.Functions.Like(l.Name, $"%{ListingFilter.SearchString}%") ||
                                             EF.Functions.Like(l.Description, $"%{ListingFilter.SearchString}%") ||
                                             EF.Functions.Like(l.Subject, $"%{ListingFilter.SearchString}%"));
                }

                // Apply filtering based on ExperienceLvlIdFilter
                if (ListingFilter.ExperienceLvlIdFilter != null)
                {
                    query = query.Where(l => l.ExperienceLvlId == ListingFilter.ExperienceLvlIdFilter);
                }

                // Apply filtering based on SubjectGroupIdFilter
                if (ListingFilter.SubjectGroupIdFilter != null)
                {
                    query = query.Where(l => l.SubjectGroupId == ListingFilter.SubjectGroupIdFilter);
                }

                // Execute the query and materialize the results into a list
                Listings = await query.ToListAsync();
            }

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = Listings.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            Listings = Listings.Skip(recSkip).Take(pager.PageSize).ToList();

            var listingImages = new List<ListingImage>();
            if (_context.ListingImages != null)
            {
                foreach (var l in Listings)
                {
                    var listImg = _context.ListingImages
                        .Where(li => li.ListingId == l.Id)
                        .ToList();

                    listingImages.AddRange(listImg);
                }
            }
            Images = listingImages;

            Pager = pager;
        }
    }
}
