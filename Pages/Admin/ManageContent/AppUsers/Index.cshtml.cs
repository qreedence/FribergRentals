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

namespace FribergRentals.Pages.Admin.ManageContent.AppUsers
{
    public class IndexModel : PageModel
    {
        private readonly IUser userRepo;
        public IndexModel(IUser userRepo)
        {
            this.userRepo = userRepo;
        }

        public IList<User> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Users = userRepo.GetAll().ToList();
        }
    }
}
