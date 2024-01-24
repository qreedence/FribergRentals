using FribergRentals.Data.Models;

namespace FribergRentals.Data.Interfaces
{
    public interface IUser
    {
        User GetById(int id);
        IEnumerable<User> GetAll();

        public void Add(User user);

        public void Edit(User user);

        public void Delete(int id);

        public User ValidateUser(string email, string password);

        public User GetUserDetails(string email);

        public void UpdateSessionToken(User user, string token);

        public User ValidateSessionToken(string token);
    }
}
