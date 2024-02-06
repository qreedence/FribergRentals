using FribergRentals.Data.Interfaces;
using FribergRentals.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergRentals.Pages.Login
{
    public class LogoutModel : PageModel
    {
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public LogoutModel(IUser userRepo, SessionUtility sessionUtility)
        {
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        public IActionResult OnGet()
        {
            string sessionToken = sessionUtility.ExtractSessionToken(HttpContext);
            var user = userRepo.ValidateSessionToken(sessionToken);
            sessionUtility.ClearSession(HttpContext);
            userRepo.UpdateSessionToken(user, null);
            return RedirectToPage("/Index");
        }
    }
}
