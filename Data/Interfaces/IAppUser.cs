using FribergRentals.Data.Models;
namespace FribergRentals.Data.Interfaces
{
    public interface IAppUser
    {
        public void AddOrder(Order order, AppUser appUser);
    }
}
