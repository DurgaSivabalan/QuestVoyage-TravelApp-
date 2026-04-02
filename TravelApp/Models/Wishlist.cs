namespace TravelApp.Models
{
    
        public class Wishlist
        {

            public int Id { get; set; }
            public int PackageId { get; set; }
            public string UserEmail { get; set; }
            public Package Package { get; set; }
        }
    }



