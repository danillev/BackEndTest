using System.ComponentModel.DataAnnotations;

namespace BackEndTest.Models
{
    public class Car
    {
        [Key]
        public int carNumber { get; set; }
        [Required]
        public string lastStationName { get; set; }
        [Required]
        public DateTime dateAndTimeLastOperation { get; set; }
        [Required]
        public string invoiceNumber { get; set; }
        [Required]
        public int positionInTrain { get; set; }
        [Required]
        public string lastOperationName { get; set; }
        [Required]
        public string freightEtsngName { get; set; }
        [Required]
        public int freightTotalWeightKg { get; set; }
    }
}
