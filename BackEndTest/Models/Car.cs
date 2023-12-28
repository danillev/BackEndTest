using System.ComponentModel.DataAnnotations;

namespace BackEndTest.Models
{
    public class Car
    {
        [Key]
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

        public Car(XmlModel model)
        {
            this.carNumber = model.CarNumber;
            this.dateAndTimeLastOperation = model.WhenLastOperation;
            this.freightEtsngName = model.FreightEtsngName;
            this.freightTotalWeightKg = model.FreightTotalWeightKg;
            this.positionInTrain = model.PositionInTrain;
            this.invoiceNumber = model.InvoiceNum;
            this.lastOperationName = model.LastOperationName;
            this.lastStationName = model.LastStationName;
        }

        public static bool operator <(Car car, XmlModel model)
        {
            return car.dateAndTimeLastOperation < model.WhenLastOperation;
        }

        public static bool operator >(Car car, XmlModel model)
        {
            return car.dateAndTimeLastOperation > model.WhenLastOperation;
        }
    }
}
