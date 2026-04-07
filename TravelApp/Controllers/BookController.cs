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

        //STORE IN SESSION
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
    public IActionResult Book(int? packageId)
    {
        var data = HttpContext.Session.GetString("BookingData");

        if (data != null)
        {
            var booking = Newtonsoft.Json.JsonConvert.DeserializeObject<Booking>(data);

            ViewBag.Packages = _context.Packages.ToList();

            return View(booking); 
        }
    
     
    
        // fallback (new booking)
        if (packageId != null)
        {
            var package = _context.Packages.FirstOrDefault(p => p.Id == packageId);
            if (package != null)
            {
                ViewBag.SelectedPackage = package;
            }
        }

        ViewBag.Packages = _context.Packages.ToList();
        return View();
    }
    public JsonResult GetPrice(int packageId)
    {
        var pkg = _context.Packages.FirstOrDefault(p => p.Id == packageId);
        return Json(new { price = pkg?.Price ?? 0 });
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
        if (booking.Travellers == null)
        {
            booking.Travellers = new List<Traveller>();
        }

        // debug
        // get the package details from db
        var package = _context.Packages
    .FirstOrDefault(p => p.Id == booking.PackageId);
        //getting package id from packages top store in booking
        if (package == null)
        {
            return Content("Package not found");
        }

        booking.PackageName = package.Name;
        booking.Price = package.Price;

        booking.Members = booking.Adults + booking.Children;
        booking.TotalAmount = booking.Price * booking.Members;

        //store full data in session for next steps
        var json = JsonConvert.SerializeObject(booking);
        HttpContext.Session.SetString("BookingData", json);

        Console.WriteLine("SESSION SAVED: " + json);

        return View("~/Views/Summary/Summary.cshtml", booking);
    }

    //confirm method after payment.
    [HttpPost]
    public IActionResult Confirm(Booking booking)
    {
        if (booking == null)
            return RedirectToAction("Book");

        // Get logged user
        booking.UserEmail = HttpContext.Session.GetString("User");

        // GET REAL PACKAGE FROM DB
        var package = _context.Packages
            .FirstOrDefault(p => p.Name == booking.PackageName);

        if (package == null)
            return BadRequest("Invalid package");

        // RE-CALCULATE TOTAL (DO NOT TRUST FRONTEND)
        var actualAmount = package.Price * booking.Members;
        booking.TotalAmount = actualAmount;

        //CONTROL PAYMENT STATUS IN BACKEND ONLY
        booking.PaymentMethod = string.IsNullOrEmpty(booking.PaymentMethod) ? "UPI" : booking.PaymentMethod;

        // ❌ IGNORE frontend PaymentStatus
        booking.PaymentStatus = "Pending"; // or "Pending" if you want stricter flow

        // STATUS MAPPING
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

        // SAVE
        _context.Bookings.Add(booking);
        _context.SaveChanges();

        // clear session
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
