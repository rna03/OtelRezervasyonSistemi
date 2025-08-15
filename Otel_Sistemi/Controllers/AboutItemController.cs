using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;
using System.Linq;

namespace Otel_Sistemi.Controllers
{
    public class AboutItemController : Controller
    {
        private readonly AppDbContext _context;

        public AboutItemController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.AboutItems.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AboutItem aboutItem)
        {
            if (ModelState.IsValid)
            {
                _context.AboutItems.Add(aboutItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutItem);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var value = _context.AboutItems.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(AboutItem aboutItem)
        {
            if (ModelState.IsValid)
            {
                _context.AboutItems.Update(aboutItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutItem);
        }

        public IActionResult Delete(int id)
        {
            var value = _context.AboutItems.Find(id);
            if (value == null) return NotFound();

            _context.AboutItems.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
