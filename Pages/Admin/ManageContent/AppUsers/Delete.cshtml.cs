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

namespace FribergRentals.Pages.Admin.ManageContent.AppUsers
{
    public class DeleteModel : PageModel
    {
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public DeleteModel(IUser userRepo, SessionUtility sessionUtility)
        {
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var user = userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
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

        public IActionResult OnPostAsync(int id)
        {
            var user = userRepo.GetById(id);
            if (user != null)
            {
                User = user;
                userRepo.Delete(id);
            }
            return RedirectToPage("./Index");
        }
    }
}
