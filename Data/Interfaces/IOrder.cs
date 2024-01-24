using System.Linq.Expressions;
using FribergRentals.Data.Models;

namespace FribergRentals.Data.Interfaces
{
    public interface IOrder
    {
        Order GetById(int id);
        IEnumerable<Order> GetByExpression(Expression<Func<Order, bool>> predicate);
        IEnumerable<Order> GetAll();

        public void Add(Order order);

        public void Edit(Order order);

        public void Delete(int id);

        public void Confirm(Order order);
    }
}
