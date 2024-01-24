using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;

namespace FribergRentals.Data.Repositories
{
    public class AppUserRepo : IAppUser
    {
        private readonly ApplicationDbContext applicationDbContext;
        public AppUserRepo(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return applicationDbContext.AppUsers.OrderBy(x => x.Id);
        }

        public AppUser GetById(int id)
        {
            return applicationDbContext.AppUsers.FirstOrDefault(x => x.Id == id);
        }

        public void Add(AppUser appUser)
        {
            applicationDbContext.AppUsers.Add(appUser);
            applicationDbContext.SaveChanges();
        }

        public void Edit(AppUser appUser)
        {
            applicationDbContext.AppUsers.Update(appUser);
            applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationDbContext.AppUsers.Remove(GetById(id));
            applicationDbContext.SaveChanges();
        }

        public AppUser ValidateUser(string email, string password)
        {
            AppUser appUser = applicationDbContext.AppUsers.FirstOrDefault(x => x.Email == email && x.Password == password);
            return appUser;
        }

        public void UpdateSessionToken(AppUser user, string token)
        {
            user.SessionToken = token;
            applicationDbContext.SaveChanges();
        }

        public AppUser ValidateSessionToken(string token)
        {
            AppUser user = applicationDbContext.AppUsers.FirstOrDefault(x => x.SessionToken == token);
            return user;
        }

        public AppUser GetUserDetails(string email)
        {
            AppUser appUser = applicationDbContext.AppUsers.FirstOrDefault(x => x.Email == email);
            return appUser;
        }

        public void AddOrder(Order order, AppUser appUser)
        {
            appUser.CustomerOrders.Add(order);
            applicationDbContext.SaveChanges();
        }
    }
}

