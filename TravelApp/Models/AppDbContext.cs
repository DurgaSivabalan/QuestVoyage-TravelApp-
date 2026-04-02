using Microsoft.EntityFrameworkCore;

namespace TravelApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Traveller> Travellers { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
    }
}