using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergRentals.Data.Repositories
{
    public class OrderRepo :IOrder
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ICar carRepo;
        private readonly IUser userRepo;
        public OrderRepo(ApplicationDbContext context, ICar carRepo, IUser userRepo)
        {
            _applicationDbContext = context;
            this.carRepo = carRepo;
            this.userRepo = userRepo;
        }

        public IEnumerable<Order> GetAll()
        {
            return _applicationDbContext.Orders
                .OrderBy(x => x.Id)
                .Include(order=>order.Car)
                .Include(order=>order.User)
                .ToList();
        }

        public Order GetById(int id)
        {
            Order order = _applicationDbContext.Orders.Include(x => x.Car).Include(x=>x.User).FirstOrDefault(x => x.Id == id);
            return order;
        }

        public IList<Order> GetOrdersWithRelatedEntities(Expression<Func<Order, bool>> predicate)
        {
            var orders = _applicationDbContext.Orders.Where(predicate).ToList();
            foreach (var order in orders)
            {
                LoadRelatedEntities(order);
            }
            return orders;
        }

        public void LoadRelatedEntities(Order order)
        {
            _applicationDbContext.Entry(order)
                .Reference(x => x.Car)
                .Load();

            _applicationDbContext.Entry(order)
                .Reference(x => x.User)
                .Load();
        }

        public void Add(Order order)
        {
            order.Car = carRepo.GetById(order.Car.Id);
            order.User = userRepo.GetById(order.User.Id);
            _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();
        }

        public void Edit(Order order)
        {
            _applicationDbContext.Orders.Update(order);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _applicationDbContext.Orders.Remove(GetById(id));
            _applicationDbContext.SaveChanges();
        }

        public void Confirm(Order order)
        {
            _applicationDbContext.SaveChanges();
        }
    }
}

