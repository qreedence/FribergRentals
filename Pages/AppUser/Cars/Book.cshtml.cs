using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using FribergRentals.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FribergRentals.Pages.AppUser.Cars
{
    public class BookModel : PageModel
    {
        private readonly IOrder orderRepo;
        private readonly ICar carRepo;
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public BookModel(IOrder orderRepo, ICar carRepo, IUser userRepo, SessionUtility sessionUtility)
        {
            this.carRepo = carRepo;
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        [BindProperty]
        public Order Order { get; set; }
        
        public Car Car { get; set; } 
        public User User { get; set; }


        public IActionResult OnGet(int id)
        {
            Car = carRepo.GetById(id);
            return Page();
        }

        public IActionResult OnPost(int carId)
        {
            Car car = carRepo.GetById(carId);
            User currentUser = userRepo.ValidateSessionToken(sessionUtility.ExtractSessionToken(HttpContext));

            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["SessionToken"]))
            {
                HttpContext.Response.Redirect("/Login/Login");
            }

            if(Order.ReturnDate.CompareTo(Order.PickUpDate) > 0)
            {
                Order.Car = car;
                Order.User = currentUser;
                Order.TotalPrice = Order.NumberOfDays * car.Price;
                Order.TimeOfOrder = DateTime.Now;
                TempData["Order"] = JsonConvert.SerializeObject(Order);
                return RedirectToPage("./BookConfirm");
            }
            else
            {
                OnGet(carId);
                TempData["DateError"] = "Return date cannot be earlier than or the same as the pick-up date";
            }
            return Page();
        }
    }

}
