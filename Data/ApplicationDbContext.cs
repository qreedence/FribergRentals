using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FribergRentals.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FribergRentals.Data.Models.Car> Cars { get; set; }
        public DbSet<FribergRentals.Data.Models.AppUser> AppUsers { get; set; }
        public DbSet<FribergRentals.Data.Models.Admin> Admins { get; set; }
        public DbSet<FribergRentals.Data.Models.Order> Orders { get; set; }
    }
}
