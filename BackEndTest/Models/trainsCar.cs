using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackEndTest.Models
{
    public record class TrainsCar
    {
        public int traintNumber { get; init; }
        public int carNumber { get; init; }

        public TrainsCar(XmlModel model)
        {
            this.carNumber = model.CarNumber;
            this.traintNumber = model.TrainNumber;
        }
    }    
}
