using BackEndTest.Data;
using BackEndTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BackEndTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SheetsJsonController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepository<Train> _trainGenericRepository;
        private readonly GenericRepository<Car> _carGenericRepository;

        public SheetsJsonController(ApplicationContext context)
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            TrainWithCars trainWithCars = new TrainWithCars();
            trainWithCars.cars = new List<Car>();
            trainWithCars.train = await _trainGenericRepository.GetById(id);
            foreach (var car in trainWithCars.train.Cars)
            {
                var carModdel = await _carGenericRepository.GetById(car);
                if(carModdel != null)
                    trainWithCars.cars.Add(carModdel);
            }
            trainWithCars.cars = trainWithCars.cars.OrderBy(car => car.positionInTrain).ToList();

            var JsonResult = JsonSerializer.Serialize(trainWithCars, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),

                WriteIndented = true
            });
            return Ok(JsonResult);
        }
    }
}
