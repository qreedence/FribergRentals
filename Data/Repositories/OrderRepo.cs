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
            return _applicationDbContext.Orders.OrderBy(x => x.Id);
        }

        public Order GetById(int id)
        {
            return _applicationDbContext.Orders.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Order> GetByExpression(Expression<Func<Order, bool>> predicate)
        {
            return _applicationDbContext.Orders
         .Where(predicate)
         .ToList();
        }

        public void Add(Order order)
        {
            DetachEntity(order.Car);
            DetachEntity(order.User);

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
            // _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            var entry = _applicationDbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}

