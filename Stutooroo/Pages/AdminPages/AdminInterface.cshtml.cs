using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stutooroo.Pages.AdminPages
{
	[Authorize(Roles = "Admin")]
	public class AdminInterfaceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
