using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergRentals.Data;
using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.Admin.ManageContent.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrder orderRepo;
        private readonly SessionUtility sessionUtility;

        public EditModel(IOrder orderRepo, SessionUtility sessionUtility)
        {
            this.orderRepo = orderRepo;
            this.sessionUtility = sessionUtility;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
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

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           orderRepo.Edit(Order);
           return RedirectToPage("./Index");
        }
    }
}
