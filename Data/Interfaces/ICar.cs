using FribergRentals.Data.Models;

namespace FribergRentals.Data.Interfaces
{
    public interface ICar
    {
        Car GetById(int id);
        IEnumerable<Car> GetAll();

        public void Add(Car car);

        public void Edit(Car car);

        public void Delete(int id);
    }
}
