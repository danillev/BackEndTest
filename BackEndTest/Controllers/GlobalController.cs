using BackEndTest.Data;
using BackEndTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTest.Controllers
{
    public class GlobalController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepository<Train> _trainGenericRepository;
        private readonly GenericRepository<Car> _carGenericRepository;
        private readonly TrainsCarGenericRepository _trainsCarGenericRepository;

        public GlobalController(ApplicationContext context)
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
            _trainsCarGenericRepository = new TrainsCarGenericRepository(_context);
        }

        protected async ValueTask<List<Car>> GetCarsByTraindNumber(int trainNumber)
        {
            List<Car> cars = new List<Car>();
            var listCars = _trainsCarGenericRepository.GetListById(trainNumber).Result;
            foreach (var car in listCars)
            {
                var carModdel = await _carGenericRepository.GetById(car.carNumber);
                if (carModdel != null)
                    cars.Add(carModdel);
            }
            return cars.OrderBy(car => car.positionInTrain).ToList();
        }
    }
}
