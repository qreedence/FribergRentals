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

namespace FribergRentals.Pages.Admin.ManageContent.AppUsers
{
    public class EditModel : PageModel
    {
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public EditModel(IUser userRepo, SessionUtility sessionUtility)
        {
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        [BindProperty]
        public FribergRentals.Data.Models.AppUser User { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var user = userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            User = (Data.Models.AppUser)user;
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
                userRepo.Edit(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
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

        private bool UserExists(int id)
        {
            return userRepo.GetById(id) != null;
        }
    }
}
