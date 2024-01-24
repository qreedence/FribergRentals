using FribergRentals.Data.Interfaces;

namespace FribergRentals.Data.Models
{
    public class AppUser : User
    {
        public virtual List<Order>? CustomerOrders { get; set; }
    }
}
