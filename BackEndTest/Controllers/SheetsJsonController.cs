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
    public class SheetsJsonController : GlobalController
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepository<Train> _trainGenericRepository;
        private readonly GenericRepository<Car> _carGenericRepository;
        private readonly TrainsCarGenericRepository _trainsCarGenericRepository;

        public SheetsJsonController(ApplicationContext context) : base(context)
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
            _trainsCarGenericRepository = new TrainsCarGenericRepository(_context);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            TrainWithCars trainWithCars = new TrainWithCars();
            trainWithCars.train = await _trainGenericRepository.GetById(id);
            trainWithCars.cars = GetCarsByTraindNumber(trainWithCars.train.trainNumber).Result;

            var JsonResult = JsonSerializer.Serialize(trainWithCars, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                WriteIndented = true
            });
            return Ok(JsonResult);
        }
    }
}
