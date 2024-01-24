using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergRentals.Pages.Login
{
    public class CreateAccountModel : PageModel
    {
        private readonly IUser userRepo;

        public CreateAccountModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FribergRentals.Data.Models.AppUser User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
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


            return RedirectToPage("/Index");
        }
    }
}
