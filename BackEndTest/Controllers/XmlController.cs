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

        public XmlController(ApplicationContext context)
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
        }

        [HttpPost("application/xml")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не выбран или пустой");
            }

            List<XmlModel> XmlModels = await DeserializeXmlFile(file);

            foreach (var model in XmlModels)
            {
                await ProcessXmlModel(model);
            }

            var json = JsonSerializer.Serialize(_trainGenericRepository.GetAll());
            return Ok(json);
        }

        private async Task<List<XmlModel>> DeserializeXmlFile(IFormFile file)
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

        private async Task ProcessXmlModel(XmlModel model)
        {
            var trainModel = await _trainGenericRepository.GetById(model.TrainNumber);

            if (trainModel != null)
            {
                await ProcessExistingTrainModel(trainModel, model);
            }
            else
            {
                await ProcessNewTrainModel(model);
            }
        }

        private async Task ProcessExistingTrainModel(Train trainModel, XmlModel model)
        {
            var carModel = await _carGenericRepository.GetById(model.CarNumber);

            if (carModel == null)
            {
                await CreateAndSaveNewCarModel(model);
            }
            else
            {
                await UpdateExistingCarModel(carModel, model);
            }

            await AddCarToTrain(trainModel, model.CarNumber);
        }

        private async Task CreateAndSaveNewCarModel(XmlModel model)
        {
            var carModel = new Car()
            {
                carNumber = model.CarNumber,
                dateAndTimeLastOperation = model.WhenLastOperation,
                freightEtsngName = model.FreightEtsngName,
                freightTotalWeightKg = model.FreightTotalWeightKg,
                positionInTrain = model.PositionInTrain,
                invoiceNumber = model.InvoiceNum,
                lastOperationName = model.LastOperationName,
                lastStationName = model.LastStationName,
            };

            _carGenericRepository.Create(carModel);
            await _carGenericRepository.Save();
        }

        private async Task UpdateExistingCarModel(Car carModel, XmlModel model)
        {
            if (carModel.dateAndTimeLastOperation < model.WhenLastOperation)
            {
                carModel.dateAndTimeLastOperation = model.WhenLastOperation;
                carModel.freightEtsngName = model.FreightEtsngName;
                carModel.freightTotalWeightKg = model.FreightTotalWeightKg;
                carModel.positionInTrain = model.PositionInTrain;
                carModel.invoiceNumber = model.InvoiceNum;
                carModel.lastOperationName = model.LastOperationName;
                carModel.lastStationName = model.LastStationName;

                _carGenericRepository.Update(carModel);
                await _carGenericRepository.Save();
            }
        }

        private async Task AddCarToTrain(Train trainModel, int carNumber)
        {
            if (!trainModel.Cars.Contains(carNumber))
            {
                trainModel.Cars.Add(carNumber);
                _trainGenericRepository.Update(trainModel);
                await _trainGenericRepository.Save();
            }
        }

        private async Task ProcessNewTrainModel(XmlModel model)
        {
            var trainModel = new Train()
            {
                fromStationName = model.FromStationName,
                toStationName = model.ToStationName,
                trainIndexCombined = model.TrainIndexCombined,
                trainNumber = model.TrainNumber,
                Cars = new List<int> { model.CarNumber }
            };

            _trainGenericRepository.Create(trainModel);
            await _trainGenericRepository.Save();
        }


        /*
        [HttpPost("application/xml")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл не выбран или пустой");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(XmlModelList));
            List<XmlModel> XmlModels;
            using (var stream = file.OpenReadStream())
            {
                XmlModelList xmlModelList = (XmlModelList)serializer.Deserialize(stream);
                if (xmlModelList == null) { return BadRequest("Stream error!"); }
                XmlModels = xmlModelList.XmlModels;
            }

            foreach (var model in XmlModels)
            {
                var trainModel = await _trainGenericRepository.GetById(model.TrainNumber);
                if (trainModel != null) 
                {
                    Car carModel = await _carGenericRepository.GetById(model.CarNumber);
                    if (carModel == null)
                    {
                        carModel = new Car()
                        {
                            carNumber = model.CarNumber,
                            dateAndTimeLastOperation = model.WhenLastOperation,
                            freightEtsngName = model.FreightEtsngName,
                            freightTotalWeightKg = model.FreightTotalWeightKg,
                            positionInTrain = model.PositionInTrain,
                            invoiceNumber = model.InvoiceNum,
                            lastOperationName = model.LastOperationName,
                            lastStationName = model.LastStationName,
                        };
                        _carGenericRepository.Create(carModel);
                        await _carGenericRepository.Save();
                    }
                    else
                    {
                        if (carModel.dateAndTimeLastOperation < model.WhenLastOperation)
                        {
                            carModel.dateAndTimeLastOperation = model.WhenLastOperation;
                            carModel.freightEtsngName = model.FreightEtsngName;
                            carModel.freightTotalWeightKg = model.FreightTotalWeightKg;
                            carModel.positionInTrain = model.PositionInTrain;
                            carModel.invoiceNumber = model.InvoiceNum;
                            carModel.lastOperationName = model.LastOperationName;
                            carModel.lastStationName = model.LastStationName;

                            _carGenericRepository.Update(carModel);
                            await _carGenericRepository.Save();
                        }

                    }

                    if (!trainModel.Cars.Contains(model.CarNumber))
                    {
                        trainModel.Cars.Add(model.CarNumber);
                        _trainGenericRepository.Update(trainModel);
                        await _trainGenericRepository.Save();
                    }
                }
                else
                {
                    trainModel = new Train()
                    {
                        fromStationName = model.FromStationName,
                        toStationName = model.ToStationName,
                        trainIndexCombined = model.TrainIndexCombined,
                        trainNumber = model.TrainNumber,
                        Cars = new List<int> { model.CarNumber }
                    };
                    _trainGenericRepository.Create(trainModel);
                    await _trainGenericRepository.Save();
                }
                
            }

            var json = JsonSerializer.Serialize(_trainGenericRepository.GetAll());

            return Ok(json);
        }
        */
    }
}
