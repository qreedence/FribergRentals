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

namespace FribergRentals.Pages.Admin.ManageContent.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ICar carRepo;
        public CreateModel(ICar carRepo)
        {
            this.carRepo = carRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                carRepo.Add(Car);
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
