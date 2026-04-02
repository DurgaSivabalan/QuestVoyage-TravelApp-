using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

public class ProfileController : Controller
{
    private readonly AppDbContext _context;
    private readonly string? userEmail;




    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Profile()
    {
        var email = HttpContext.Session.GetString("User");

        if (email == null)
            return RedirectToAction("Login", "Login");

        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        var bookings = _context.Bookings
    .Where(b => b.UserEmail == email)
    .OrderByDescending(b => b.Id)   // latest first
    .Take(3)                        // only last 3
    .ToList();

        ViewBag.User = user;
        ViewBag.Bookings = bookings;

        return View();
    }
    [HttpPost]
    public IActionResult UpdateProfile(int Id, string Name, string Phone, string RemovePhoto, IFormFile ImageFile)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == Id);

        if (user == null)
        {
            return Content("User not found");
        }

        user.Name = Name;
        user.Phone = Phone;

        // 🔴 REMOVE PHOTO
        if (RemovePhoto == "true")
        {
            user.ProfileImage = null;
        }

        // 🟢 UPLOAD NEW PHOTO (THIS MUST COME AFTER REMOVE CHECK)
        if (ImageFile != null && ImageFile.Length > 0)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }

            user.ProfileImage = "/images/" + fileName;
        }

        _context.SaveChanges();

        return RedirectToAction("Profile");
    }
}