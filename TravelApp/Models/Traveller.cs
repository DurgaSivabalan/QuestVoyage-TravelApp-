namespace TravelApp.Models
{
    public class Traveller
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}