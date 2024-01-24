using FribergRentals.Data.Models;
using FribergRentals.Data.Interfaces;

namespace FribergRentals.Data.Repositories
{
    public class UserRepo : IUser
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UserRepo(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        public IEnumerable<User> GetAll()
        {
            return applicationDbContext.AppUsers.OrderBy(x => x.Id);
        }

        public User GetById(int id)
        {
            var appUser = applicationDbContext.AppUsers.FirstOrDefault(x => x.Id == id);
            if (appUser != null)
            {
                return appUser;
            }
            var admin = applicationDbContext.Admins.FirstOrDefault(x => x.Id == id);
            return admin;
        }

        public void Add(User user)
        {
            if (user is AppUser appUser)
            {
                applicationDbContext.AppUsers.Add(appUser);
            }
            else if (user is Admin admin)
            {
                applicationDbContext.Admins.Add(admin);
            }
            applicationDbContext.SaveChanges();
        }

        public void Edit(User user)
        {
            if (user is AppUser appUser)
            {
                applicationDbContext.AppUsers.Update(appUser);
            }
            else if (user is Admin admin)
            {
                applicationDbContext.Admins.Update(admin);
            }
            applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var appUser = applicationDbContext.AppUsers.FirstOrDefault(x => x.Id == id);
            if (appUser != null)
            {
                applicationDbContext.Remove(appUser);
            }
            else
            {
                var admin = applicationDbContext.Admins.FirstOrDefault(y => y.Id == id);
                if (admin != null)
                {
                    applicationDbContext.Admins.Remove(admin);
                }
            }
            applicationDbContext.SaveChanges();
        }

        public User ValidateUser(string email, string password)
        {
            User user = null;
            var appUser = applicationDbContext.AppUsers.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (appUser != null)
            {
                user = appUser;
                return user;
            }
            var admin = applicationDbContext.Admins.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (admin != null)
            {
                user = admin;
                return user;
            }
            return user;
        }

        public void UpdateSessionToken(User user, string token)
        {
            user.SessionToken = token;
            applicationDbContext.SaveChanges();
        }

        public User ValidateSessionToken(string token)
        {
            User user = applicationDbContext.AppUsers.FirstOrDefault(x => x.SessionToken == token);
            if (user != null)
            {
                return user;
            }
            else
            {
                user = applicationDbContext.Admins.FirstOrDefault(x => x.SessionToken == token);
                return user;
            }
            return user;
        }

        public User GetUserDetails(string email)
        {
            User user = applicationDbContext.AppUsers.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                return user;
            }
            else
            {
                user = applicationDbContext.Admins.FirstOrDefault(x => x.Email == email);
                return user;
            }
            return user;
        }
    }
}
