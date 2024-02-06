using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FribergRentals.Data;
using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;
using FribergRentals.Utilities;

namespace FribergRentals.Pages.Admin.ManageContent.Admins
{
    public class CreateModel : PageModel
    {
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public CreateModel(IUser userRepo, SessionUtility sessionUtility)
        {
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;

        }

        public IActionResult OnGet()
        {
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
                else if (userAuthLevel =="user")
                {
                    HttpContext.Response.Redirect("/Login/SessionExpired");
                }
            } 
            return Page();
        }

        [BindProperty]
        public FribergRentals.Data.Models.Admin Admin { get; set; } = default!;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            userRepo.Add(Admin);

            return RedirectToPage("./Index");
        }
    }
}
