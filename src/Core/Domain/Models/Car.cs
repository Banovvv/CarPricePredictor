using Domain.Models.Enumerations;
using Microsoft.ML.Data;

namespace Domain.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Body { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int HorsePower { get; set; }
        public int Fuel { get; set; }
        public int Transmission { get; set; }
        public int EmissionStandard { get; set; }
        public int Price { get; set; }
    }
}
