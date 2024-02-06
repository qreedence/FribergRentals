using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergRentals.Pages.AppUser.Cars
{
    public class BrowseModel : PageModel
    {
        private readonly ICar carRepo;
        public BrowseModel(ICar carRepo)
        {
            this.carRepo = carRepo;
        }

        public IList<Car> Car { get; set; } = default!;

        public IActionResult OnGet()
        {
            Car = carRepo.GetAll().ToList();
            return Page();
        }
    }
}
