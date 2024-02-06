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

        public LoginModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        [BindProperty]
        [Required]
        [DisplayName("E-Mail")]
        public string LoginEmail { get; set; }
        [BindProperty]
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = userRepo.ValidateUser(LoginEmail, LoginPassword);
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

        [BindProperty]
        public FribergRentals.Data.Models.AppUser User { get; set; } = default!;

        public IActionResult OnPostCreateAccount()
        {
            //ModelState funkar inte, räknar med Login-fälten också
            if (string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password) || string.IsNullOrEmpty(User.VerifyPassword))
            {
                return Page();
            }

            try
            {
                userRepo.Add(User);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return RedirectToPage("./CreateSuccessful");
        }
    }
}

