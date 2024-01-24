using FribergRentals.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergRentals.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IUser userRepo;

        [BindProperty]
        [Required]
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = userRepo.ValidateUser(Email, Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, ("Invalid username or password"));
                return Page();
            }

            if (user != null)
            {
                var sessionToken = GenerateSessionToken();
                userRepo.UpdateSessionToken(user, sessionToken);
                Response.Cookies.Append("SessionToken", sessionToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddMinutes(20)
                });
            }
            return RedirectToPage("LoginSuccessful");
        }

        public string GenerateSessionToken()
        {
            var token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}
