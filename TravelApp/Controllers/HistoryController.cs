using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class HistoryController : Controller
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult History()
        {
            var bookings = _context.Bookings.ToList();
            return View(bookings); // no path here
        }
    }
    }
