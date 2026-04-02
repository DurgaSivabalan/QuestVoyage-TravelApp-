namespace TravelApp.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Type { get; set; }     

        public int Duration { get; set; }

        public string? Description { get; set; }    

        public byte[]? ImageData { get; set; }   // 🔥 image stored here

    }
}
        