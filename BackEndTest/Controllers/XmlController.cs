using BackEndTest.Data;
using BackEndTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BackEndTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class XmlController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepository<Train> _trainGenericRepository;
        private readonly GenericRepository<Car> _carGenericRepository;
        private readonly TrainsCarGenericRepository _trainsCarGenericRepository;

        public XmlController(ApplicationContext context)
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
            _trainsCarGenericRepository = new TrainsCarGenericRepository(_context);
        }

        [HttpPost("application/xml")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new Exception("Файл не выбран или пустой");
                }
                List<XmlModel> XmlModels = await DeserializeXmlFile(file);
                if (XmlModels == null)
                {
                    throw new Exception("Файл содержит неверный формат");
                }

                foreach (var model in XmlModels)
                {
                    ProcessXmlModel(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        private async ValueTask<List<XmlModel>> DeserializeXmlFile(IFormFile file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlModelList));
            List<XmlModel> XmlModels;
            using (var stream = file.OpenReadStream())
            {
                XmlModelList xmlModelList = (XmlModelList)serializer.Deserialize(stream);
                if (xmlModelList == null) { throw new InvalidOperationException("Stream error!"); }
                XmlModels = xmlModelList.XmlModels;
            }
            return XmlModels;
        }

        private void ProcessXmlModel(XmlModel model)
        {
            var trainModel = _trainGenericRepository.GetById(model.TrainNumber).Result;

            if (trainModel != null)
            {
                ProcessExistingTrainModel(trainModel, model);
            }
            else
            {
                ProcessNewTrainModel(model);
            }
        }

        private async Task ProcessExistingTrainModel(Train trainModel, XmlModel model)
        {
            var carModel = _carGenericRepository.GetById(model.CarNumber).Result;

            if (carModel == null)
            {
                await CreateAndSaveNewCarModel(model);
            }
            else
            {
                await UpdateExistingCarModel(carModel, model);
            }

            await AddCarToTrain(model);
        }

        private async Task CreateAndSaveNewCarModel(XmlModel model)
        {
            _carGenericRepository.Create(new Car(model));
            await _carGenericRepository.Save();
        }

        private async Task UpdateExistingCarModel(Car carModel, XmlModel model)
        {
            if (carModel < model)
            {
                _carGenericRepository.Update(new Car(model));
                await _carGenericRepository.Save();
            }
        }

        private async Task AddCarToTrain(XmlModel model)
        {
            if (_trainsCarGenericRepository.GetTrainsCar(model.TrainNumber, model.CarNumber).Result == null)
            {
                _trainsCarGenericRepository.Create(new TrainsCar(model));
                await _carGenericRepository.Save();
            }
        }

        private async Task ProcessNewTrainModel(XmlModel model)
        {
            _trainsCarGenericRepository.Create(new TrainsCar(model));
            _trainGenericRepository.Create(new Train(model));
            await _trainGenericRepository.Save();
        }

    }
}
