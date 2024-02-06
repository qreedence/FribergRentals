using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergRentals.Data;
using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.Admin.ManageContent.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ICar carRepo;
        private readonly SessionUtility sessionUtility;
        public IndexModel(ICar carRepo, SessionUtility sessionUtility)
        {
            this.carRepo = carRepo;
            this.sessionUtility = sessionUtility;
        }

        public IList<Car> Car { get; set; } = default!;

        public IActionResult OnGet()
        {
            Car = carRepo.GetAll().ToList();
            
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["SessionToken"]))
            {
                HttpContext.Response.Redirect("/Login/SessionExpired");
            }
            else
            {
                string userAuthLevel = sessionUtility.CheckUser(HttpContext);
                if (userAuthLevel == "admin")
                {
                    Car = carRepo.GetAll().ToList();
                }
                else if (userAuthLevel == "user")
                {
                    HttpContext.Response.Redirect("/Login/SessionExpired");
                }
            }
            return Page();
        }
    }
}
