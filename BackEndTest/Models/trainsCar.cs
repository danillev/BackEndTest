using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackEndTest.Models
{
    public record class trainsCar
    {
        public int traintNumber { get; init; }
        public int carNumber { get; init; }
    }
}
