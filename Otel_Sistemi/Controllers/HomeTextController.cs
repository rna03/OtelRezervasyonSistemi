using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;

namespace Otel_Sistemi.Controllers
{
    public class HomeTextController : Controller
    {
        private readonly AppDbContext _context;

        public HomeTextController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.HomeTexts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeText model)
        {
            if (ModelState.IsValid)
            {
                _context.HomeTexts.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var value = _context.HomeTexts.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(HomeText model)
        {
            if (ModelState.IsValid)
            {
                _context.HomeTexts.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var value = _context.HomeTexts.Find(id);
            if (value == null) return NotFound();
            _context.HomeTexts.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
