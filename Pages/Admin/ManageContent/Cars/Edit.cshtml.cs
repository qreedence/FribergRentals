using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergRentals.Data;
using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.Admin.ManageContent.Cars
{
    public class EditModel : PageModel
    {
        private readonly ICar carRepo;
        private readonly SessionUtility sessionUtility;
        public EditModel(ICar carRepo, SessionUtility sessionUtility)
        {
            this.carRepo = carRepo;
            this.sessionUtility = sessionUtility;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var car = carRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            Car = car;
            if (string.IsNullOrEmpty(HttpContext.Request.Cookies["SessionToken"]))
            {
                HttpContext.Response.Redirect("/Login/SessionExpired");
            }
            else
            {
                string userAuthLevel = sessionUtility.CheckUser(HttpContext);
                if (userAuthLevel == "admin")
                {
                    return Page();
                }
                else if (userAuthLevel == "user")
                {
                    HttpContext.Response.Redirect("/Login/SessionExpired");
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                carRepo.Edit(Car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
            return carRepo.GetById(id) != null;
        }
    }
}
