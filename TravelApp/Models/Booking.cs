using System.ComponentModel.DataAnnotations;
namespace TravelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string? UserEmail { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }

        public string? PackageName { get; set; }
public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public int PackageId { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Members { get; set; }
        public string? SpecialRequest { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? Process { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public List<Traveller> Travellers { get; set; }= new List<Traveller>();

    }
}