using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndTest.Models
{
    [PrimaryKey(nameof(carNumber))]
    public class Car
    {
        public int carNumber { get; init; }
        [Required]
        public string lastStationName { get; init; }
        [Required]
        public DateTime dateAndTimeLastOperation { get; init; }
        [Required]
        public string invoiceNumber { get; init; }
        [Required]
        public int positionInTrain { get; init; }
        [Required]
        public string lastOperationName { get; init; }
        [Required]
        public string freightEtsngName { get; init; }
        [Required]
        public int freightTotalWeightKg { get; init; }
        
        public static bool operator <(Car car, XmlModel model)
        {
            return car.dateAndTimeLastOperation < model.WhenLastOperation;
        }

        public static bool operator >(Car car, XmlModel model)
        {
            return car.dateAndTimeLastOperation > model.WhenLastOperation;
        }

        public Car()
        {

        }

        public Car(XmlModel model)
        {
            carNumber = model.CarNumber;
            dateAndTimeLastOperation = model.WhenLastOperation;
            invoiceNumber = model.InvoiceNum;
            lastOperationName = model.LastOperationName;
            lastStationName = model.LastStationName;
            freightEtsngName = model.FreightEtsngName;
            freightTotalWeightKg = model.FreightTotalWeightKg;
            positionInTrain = model.PositionInTrain;
        }
    }
}
