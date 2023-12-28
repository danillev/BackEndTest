using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BackEndTest.Models
{
    [PrimaryKey(nameof(trainNumber))]
    public record class Train
    {
        public int trainNumber { get; init; }
        [Required]
        public string trainIndexCombined { get; init; }
        [Required]
        public string fromStationName { get; init; }
        [Required]
        public string toStationName { get; init; }
        
        public Train()
        {

        }

        public Train(XmlModel model)
        {
            fromStationName = model.FromStationName;
            toStationName = model.ToStationName;
            trainIndexCombined = model.TrainIndexCombined;
            trainNumber = model.TrainNumber;
            
        }
    }
}
