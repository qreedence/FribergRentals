using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergRentals.Pages.Login
{
    public class SessionExpiredModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
