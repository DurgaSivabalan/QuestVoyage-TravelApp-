using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;

public class ContactController : Controller
{
    private readonly AppDbContext _context;

    public ContactController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Submit(Contact contact)
    {
        if (contact == null)
            return Json(new { success = false });

        _context.Contacts.Add(contact);
        _context.SaveChanges();

        return Json(new
        {
            success = true,
            message = "Your form has been submitted. Our team will contact you soon."
        });
    }
}