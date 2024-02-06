using System.Linq.Expressions;
using FribergRentals.Data.Models;

namespace FribergRentals.Data.Interfaces
{
    public interface IOrder
    {
        Order GetById(int id);
        IEnumerable<Order> GetAll();
        public IList<Order> GetOrdersWithRelatedEntities(Expression<Func<Order, bool>> predicate);
        void LoadRelatedEntities(Order order);

        public void Add(Order order);

        public void Edit(Order order);

        public void Delete(int id);

        public void Confirm(Order order);
    }
}
