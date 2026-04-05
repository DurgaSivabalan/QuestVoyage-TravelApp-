using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Admin()
        {
            var bookings = _context.Bookings.ToList();
            
            ViewBag.WishlistCount = _context.Wishlists.Count();

            ViewBag.UserStats = _context.Users.Select(u => new
            {
                Email = u.Email,
                BookingCount = _context.Bookings.Count(b => b.UserEmail == u.Email),
                WishlistCount = _context.Wishlists.Count(w => w.UserEmail == u.Email)
            }).ToList();

            ViewBag.Contacts = _context.Contacts.ToList();
            ViewBag.Subscribers = _context.Subscribers.ToList();
            ViewBag.Packages = _context.Packages.ToList();
            // 🔥 ADD THIS (YOUR FIX)
            ViewBag.Reviews = _context.Reviews.ToList();

            return View(bookings);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);

            if (booking != null)
            {
                booking.Status = status;

                // 🔥 ALSO UPDATE PROCESS (IMPORTANT)
                if (status == "Confirmed")
                    booking.Process = "Completed";
                else if (status == "Cancelled")
                    booking.Process = "Cancelled";
                else
                    booking.Process = "Pending";

                _context.SaveChanges();
            }

            return RedirectToAction("Admin");
        }
        [HttpPost]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

            return RedirectToAction("Admin");
        }
        [HttpPost]
        public IActionResult UpdateProcess(int id, string process)
        {
            var booking = _context.Bookings.Find(id);

            if (booking != null)
            {
                booking.Process = process;
                _context.SaveChanges();
            }

            return Ok();
        }
     
        [HttpPost]
        public IActionResult DeleteSubscriber(int id)
        {
            var sub = _context.Subscribers.FirstOrDefault(x => x.Id == id);

            if (sub != null)
            {
                _context.Subscribers.Remove(sub);
                _context.SaveChanges();
            }

            return RedirectToAction("Admin");
        }
        [HttpPost]
        public IActionResult UpdatePayment(int id, string payment)
        {
            var booking = _context.Bookings.Find(id);

            if (booking != null)
            {
                booking.PaymentStatus = payment;
                _context.SaveChanges();
            }

            return Ok();
        }
        public IActionResult DownloadBackup()
        {
            var data = _context.Bookings.ToList();

            var json = System.Text.Json.JsonSerializer.Serialize(data);

            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(bytes, "application/json", "BookingBackup.json");
        }
        [HttpPost]
        public IActionResult DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges(); // 🔥 VERY IMPORTANT
            }

            return RedirectToAction("Admin");
        }

        // 🔥 CUSTOMER REVIEWS PAGE
        public IActionResult Reviews()
        {
            var reviews = _context.Reviews
                                 .OrderByDescending(r => r.Id)
                                 .ToList();

            return View(reviews);
        }

        [HttpPost]
        public IActionResult ClearBookings()
        {
            var allBookings = _context.Bookings.ToList();
            _context.Bookings.RemoveRange(allBookings);
            _context.SaveChanges();
            return RedirectToAction("Admin");
        }

        // 🗑 DELETE REVIEW (optional)
        [HttpPost]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }

            return RedirectToAction("Admin"); // or your dashboard action name
        }
    }
}