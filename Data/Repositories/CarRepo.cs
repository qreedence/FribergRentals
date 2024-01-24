using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Models;

namespace FribergRentals.Data.Repositories
{
    public class CarRepo : ICar
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CarRepo(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return _applicationDbContext.Cars.OrderBy(x => x.Id);
        }

        public Car GetById(int id)
        {
            return _applicationDbContext.Cars.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Car car)
        {
            _applicationDbContext.Cars.Add(car);
            _applicationDbContext.SaveChanges();
        }

        public void Edit(Car car)
        {
            _applicationDbContext.Cars.Update(car);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _applicationDbContext.Cars.Remove(GetById(id));
            _applicationDbContext.SaveChanges();
        }
    }
}

