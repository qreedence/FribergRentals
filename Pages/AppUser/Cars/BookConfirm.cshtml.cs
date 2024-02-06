using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using Newtonsoft.Json;
using FribergRentals.Data.Repositories;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.AppUser.Cars
{
    public class BookConfirmModel : PageModel
    {
        private readonly IOrder orderRepo;

        public BookConfirmModel (IOrder orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        public Order Order { get; set; }

        public IActionResult OnGet()
        {
            var orderFromTempData = TempData["Order"] as string;
            Order = JsonConvert.DeserializeObject<Order>(orderFromTempData);
            TempData["Order"] = JsonConvert.SerializeObject(Order);
            return Page();
        }

        public IActionResult OnPost()
        {
            var orderFromTempData = TempData["Order"] as string;
            Order = JsonConvert.DeserializeObject<Order>(orderFromTempData);
            orderRepo.Add(Order);
            return RedirectToPage("./OrderComplete");
        }
    }
}
