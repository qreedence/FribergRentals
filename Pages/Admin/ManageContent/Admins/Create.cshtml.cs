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

namespace FribergRentals.Pages.Admin.ManageContent.Admins
{
    public class CreateModel : PageModel
    {
        private readonly IUser userRepo;

        public CreateModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FribergRentals.Data.Models.Admin Admin { get; set; } = default!;

        public async Task<IActionResult> OnPost()
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
