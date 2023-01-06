using Domain.Models.Enumerations;

namespace Domain.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public Body Body { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public int HorsePower { get; set; }
        public Fuel Fuel { get; set; }
        public Transmission Transmission { get; set; }
        public EmissionStandard EmissionStandard { get; set; }
        public decimal Price { get; set; }
    }
}
