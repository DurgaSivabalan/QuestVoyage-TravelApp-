using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;
using Newtonsoft.Json;
//Dependency Injection for DB Context
public class BookController : Controller
{
    private readonly AppDbContext _context;

    public BookController(AppDbContext context)
    {
        _context = context;
    }

    //Model Binding for Booking form
    [HttpPost]
    public IActionResult Book(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Packages = _context.Packages.ToList();
            return View(booking);
        }

        // 🔥 STORE IN SESSION
        var json = JsonConvert.SerializeObject(booking);
        HttpContext.Session.SetString("BookingData", json);

        return RedirectToAction("Summary");
    }
    //Delete Booking {Delete booking based on id and user}
    [HttpPost]
    public IActionResult DeleteBooking(int id)
    {
        var email = HttpContext.Session.GetString("User");

        var booking = _context.Bookings
            .FirstOrDefault(b => b.Id == id && b.UserEmail == email);

        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }

        return RedirectToAction("History");
    }
  
    // OPEN BOOK PAGE
    public IActionResult Book()
    {
        var data = HttpContext.Session.GetString("BookingData");

        // Send Packages list to View
        ViewBag.Packages = _context.Packages.ToList();

        if (data != null)
        {
            var booking = JsonConvert.DeserializeObject<Booking>(data);
            return View(booking);
        }

        return View(new Booking());
    }

    // STEP 1 → SUMMARY{Showing Summary}
    [HttpGet]
    public IActionResult Summary()
    {
        var data = HttpContext.Session.GetString("BookingData");

        if (data != null)
        {
            var booking = JsonConvert.DeserializeObject<Booking>(data);
            return View("~/Views/Summary/Summary.cshtml", booking);
        }

        return RedirectToAction("Book");
    }

    [HttpPost]
    public IActionResult Summary(Booking booking)
    {
        // 🔥 IMPORTANT FIX
        if (booking.Travellers == null)
        {
            booking.Travellers = new List<Traveller>();
        }

        // DEBUG
        //Console.WriteLine("Traveller count: " + booking.Travellers.Count);

        var package = _context.Packages
    .FirstOrDefault(p => p.Id == booking.PackageId);

        if (package == null)
        {
            return Content("Package not found");
        }

        booking.PackageName = package.Name;
        booking.Price = package.Price;

        booking.Members = booking.Adults + booking.Children;
        booking.TotalAmount = booking.Price * booking.Members;

        // 🔥 STORE FULL DATA
        var json = JsonConvert.SerializeObject(booking);
        HttpContext.Session.SetString("BookingData", json);

        return View("~/Views/Summary/Summary.cshtml", booking);
    }
    [HttpPost]
    public IActionResult Confirm(Booking booking)
    {
        if (booking == null)
            return RedirectToAction("Book");

        // 🔥 DEBUG (check value)
        Console.WriteLine("Final Amount Received: " + booking.TotalAmount);

        booking.UserEmail = HttpContext.Session.GetString("User");

        // defaults
        booking.PaymentMethod ??= "UPI";
        booking.PaymentStatus ??= "Pending";

        booking.Status = booking.PaymentStatus switch
        {
            "Paid" => "Confirmed",
            "Failed" => "Failed",
            _ => "Pending"
        };

        booking.Process = "Completed";

        // reset traveller IDs
        if (booking.Travellers != null)
        {
            foreach (var t in booking.Travellers)
            {
                t.Id = 0;
            }
        }

        // 🔥 SAVE
        _context.Bookings.Add(booking);
        _context.SaveChanges();

        // optional: clear session
        HttpContext.Session.Remove("BookingData");

        return RedirectToAction("Invoice", new { id = booking.Id });
    }
    [HttpGet]
    public IActionResult Payment()
    {
        var data = HttpContext.Session.GetString("BookingData");

        if (data != null)
        {
            var booking = JsonConvert.DeserializeObject<Booking>(data);
            return View("~/Views/Payment/Payment.cshtml", booking);
        }

        return RedirectToAction("Book");
    }
  
    public IActionResult Invoice(int id)
    {
        var booking = _context.Bookings
            .Include(b => b.Travellers)
            .FirstOrDefault(b => b.Id == id);

        if (booking == null)
            return Content("Booking not found");

        return View("~/Views/Invoice/Invoice.cshtml", booking);
    }
    public IActionResult History()
    {
        var email = HttpContext.Session.GetString("User");

        var bookings = _context.Bookings
            .Where(b => b.UserEmail == email)
            .ToList(); 
        return View(bookings);
    }

}
