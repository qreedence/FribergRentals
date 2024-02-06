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

namespace FribergRentals.Pages.AppUser.Orders
{
    public class MyOrdersModel : PageModel
    {
        private readonly IOrder orderRepo;
        private readonly IUser userRepo;
        private readonly SessionUtility sessionUtility;

        public MyOrdersModel(IOrder orderRepo, IUser userRepo, SessionUtility sessionUtility)
        {
            this.orderRepo = orderRepo;
            this.userRepo = userRepo;
            this.sessionUtility = sessionUtility;
        }

        public IList<Order> Order { get;set; } = default!;

        public IList<Order> PastOrders { get;set; } = default!;

        public IList<Order> UpcomingOrders { get;set; } = default!;

        public User currentUser { get; set; }

        public IActionResult OnGet()
        {
            currentUser = userRepo.ValidateSessionToken(sessionUtility.ExtractSessionToken(HttpContext));
            
            UpcomingOrders = orderRepo.GetOrdersWithRelatedEntities(
                x => x.User.Id == currentUser.Id && x.PickUpDate >= DateTime.Now.Date);

            PastOrders = orderRepo.GetOrdersWithRelatedEntities(
                x=>x.User.Id == currentUser.Id && x.PickUpDate < DateTime.Now.Date);

            return Page();
        }
    }
}
