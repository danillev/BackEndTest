using BackEndTest.Data;
using BackEndTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Diagnostics;

namespace BackEndTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SheetsExcelController : GlobalController
    {
        private readonly ApplicationContext _context;
        private readonly GenericRepository<Train> _trainGenericRepository;
        private readonly GenericRepository<Car> _carGenericRepository;
        private readonly TrainsCarGenericRepository _trainsCarGenericRepository;

        public SheetsExcelController(ApplicationContext context): base(context) 
        {
            _context = context;
            _carGenericRepository = new GenericRepository<Car>(_context);
            _trainGenericRepository = new GenericRepository<Train>(_context);
            _trainsCarGenericRepository = new TrainsCarGenericRepository(_context);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Train train = await _trainGenericRepository.GetById(id);
            List<Car> cars = GetCarsByTraindNumber(train.trainNumber).Result;
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "NL_Template.xlsx");
            using(var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                Dictionary<string, GoodsInfo> goodsInfo = new Dictionary<string, GoodsInfo>();
                var worksheet = package.Workbook.Worksheets[0];
                int row = 7;

                SetTrainInfo(worksheet, train, cars);
                foreach (var car in cars)
                {
                    SetCarInfo(worksheet, car, row);
                    row++;
                    UpdateGoodsInfo(goodsInfo, car);
                }
                SetGoodsInfoSummary(worksheet, goodsInfo, row);
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", id + "_TrainData.xlsx");

            }
        }

        

        private void SetTrainInfo(ExcelWorksheet worksheet, Train train, List<Car> cars)
        {
            worksheet.Cells["C3"].Value = train.trainNumber;
            worksheet.Cells["C4"].Value = train.trainIndexCombined;
            worksheet.Cells["E3"].Value = cars[0].lastStationName;
            worksheet.Cells["E4"].Value = cars[0].dateAndTimeLastOperation.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private void SetCarInfo(ExcelWorksheet worksheet, Car car, int row)
        {
            worksheet.Cells[$"A{row}"].Value = car.positionInTrain;
            worksheet.Cells[$"B{row}"].Value = car.carNumber;
            worksheet.Cells[$"C{row}"].Value = car.invoiceNumber;
            worksheet.Cells[$"D{row}"].Value = car.dateAndTimeLastOperation.ToString("dd-MM-yyyy HH:mm:ss");
            worksheet.Cells[$"E{row}"].Value = car.freightEtsngName;
            worksheet.Cells[$"F{row}"].Value = car.freightTotalWeightKg;
            worksheet.Cells[$"G{row}"].Value = car.lastOperationName;
        }

        private void SetGoodsInfoSummary(ExcelWorksheet worksheet, Dictionary<string, GoodsInfo> goodsInfo, int row)
        {
            int sumCars = row - 7;
            double sumTotalWeight = 0;

            foreach (var goods in goodsInfo)
            {
                worksheet.Cells[$"B{row}"].Value = goods.Value.Count;
                worksheet.Cells[$"E{row}"].Value = goods.Key;
                worksheet.Cells[$"F{row}"].Value = goods.Value.TotalWeight;
                sumTotalWeight += goods.Value.TotalWeight;
                row++;
            }
            worksheet.Cells[$"A{row}"].Value = "Всего: ";
            worksheet.Cells[$"B{row}"].Value = sumCars;
            worksheet.Cells[$"E{row}"].Value = goodsInfo.Count;
            worksheet.Cells[$"F{row}"].Value = sumTotalWeight;
        }

        private void UpdateGoodsInfo(Dictionary<string, GoodsInfo> goodsInfo, Car car)
        {
            if (goodsInfo.ContainsKey(car.freightEtsngName))
            {
                goodsInfo[car.freightEtsngName].Count++;
                goodsInfo[car.freightEtsngName].TotalWeight += car.freightTotalWeightKg;
            }
            else
            {
                goodsInfo.Add(car.freightEtsngName, new GoodsInfo { Count = 1, TotalWeight = car.freightTotalWeightKg });
            }
        }

    }
}
