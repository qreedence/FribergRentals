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

namespace FribergRentals.Pages.AppUser.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly ICar carRepo;

        public DetailsModel(ICar carRepo)
        {
            this.carRepo = carRepo;
        }

        public Car Car { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var car = carRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                Car = car;
            }
            return Page();
        }
    }
}
