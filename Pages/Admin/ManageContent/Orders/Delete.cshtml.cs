using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FribergRentals.Data;
using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using FribergRentals.Data.Repositories;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.Admin.ManageContent.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IOrder orderRepo;
        private readonly SessionUtility sessionUtility;

        public DeleteModel(IOrder orderRepo, SessionUtility sessionUtility)
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
            else
            {
                Order = order;
            }
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

        public IActionResult OnPost(int id)
        {
            var order=orderRepo.GetById(id);
            if (order != null)
            {
                Order = order;
                orderRepo.Delete(id);
            }
            return RedirectToPage("./Index");
        }
    }
}
