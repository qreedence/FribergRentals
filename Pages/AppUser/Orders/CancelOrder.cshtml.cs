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

namespace FribergRentals.Pages.AppUser.Orders
{
    public class CancelOrderModel : PageModel
    {
        private readonly IOrder orderRepo;

        public CancelOrderModel(IOrder orderRepo)
        {
            this.orderRepo = orderRepo;
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
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var order = orderRepo.GetById(id);
            if (order != null)
            {
                Order = order;
                orderRepo.Delete(id);
            }
            return RedirectToPage("./MyOrders");
        }
    }
}
