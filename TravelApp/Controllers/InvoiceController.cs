using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Invoice()
        {
            return View();
        }
      
    }
}
