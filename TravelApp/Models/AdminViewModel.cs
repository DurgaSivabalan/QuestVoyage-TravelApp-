using System.Collections.Generic;
using System;
namespace TravelApp.Models
{
    public class AdminViewModel
    {
        public List<Booking> Bookings { get; set; }
        public List<User> Users { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Subscriber> Subscribers { get; set; }
        public int TotalWishlistItems { get; set; }
    }
}
