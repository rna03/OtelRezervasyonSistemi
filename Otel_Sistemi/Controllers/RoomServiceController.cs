using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;

namespace Otel_Sistemi.Controllers
{
    public class RoomServiceController : Controller
    {
        private readonly AppDbContext _context;

        public RoomServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.RoomServices.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomService model)
        {
            if (ModelState.IsValid)
            {
                _context.RoomServices.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var value = _context.RoomServices.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(RoomService model)
        {
            if (ModelState.IsValid)
            {
                _context.RoomServices.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var value = _context.RoomServices.Find(id);
            if (value == null) return NotFound();
            _context.RoomServices.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
