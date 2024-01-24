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

namespace FribergRentals.Pages.Admin.ManageContent.AppUsers
{
    public class EditModel : PageModel
    {
        private readonly IUser userRepo;

        public EditModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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
