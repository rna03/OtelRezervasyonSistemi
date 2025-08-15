using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;

namespace Otel_Sistemi.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly AppDbContext _context;

        public ContactMessageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.ContactMessages
                .OrderByDescending(x => x.Tarih)
                .ToList();
            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var value = _context.ContactMessages.Find(id);
            if (value == null) return NotFound();

            _context.ContactMessages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
