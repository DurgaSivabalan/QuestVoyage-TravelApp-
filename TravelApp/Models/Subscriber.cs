namespace TravelApp.Models
{
    public class Subscriber
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime SubscribedOn { get; set; } = DateTime.Now;
    }
}
