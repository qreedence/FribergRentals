using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using FribergRentals.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergRentals.Pages.Login
{
    public class LoginSuccessfulModel : PageModel
    {
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public User User { get; set; }

        public LoginSuccessfulModel(IUser userRepo, SessionUtility sessionUtility)
        {
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        public IActionResult OnGet()
        {
            var sessionToken = sessionUtility.ExtractSessionToken(HttpContext);
            User = userRepo.ValidateSessionToken(sessionToken);
            return Page();
        }
    }
}
