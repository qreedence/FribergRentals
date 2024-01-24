using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;

namespace FribergRentals.Data.Repositories
{
    public class AdminRepo : IAdmin
    {
        private readonly ApplicationDbContext applicationDbContext;
        public AdminRepo(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return applicationDbContext.Admins.OrderBy(x => x.Id);
        }

        public Admin GetById(int id)
        {
            return applicationDbContext.Admins.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Admin admin)
        {
            applicationDbContext.Admins.Add(admin);
            applicationDbContext.SaveChanges();
        }

        public void Edit(Admin admin)
        {
            applicationDbContext.Admins.Update(admin);
            applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationDbContext.Admins.Remove(GetById(id));
            applicationDbContext.SaveChanges();
        }

        public Admin ValidateUser(string email, string password)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(x => x.Email == email && x.Password == password);
            return admin;
        }

        public Admin GetUserDetails(string email)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(x => x.Email == email);
            return admin;
        }
        public void UpdateSessionToken(Admin admin, string token)
        {
            admin.SessionToken = token;
            applicationDbContext.SaveChanges();
        }

        public Admin ValidateSessionToken(string token)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(x => x.SessionToken == token);
            return admin;
        }
    }
}

