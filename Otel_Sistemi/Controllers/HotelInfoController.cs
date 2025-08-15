using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;
using System.Linq;

namespace Otel_Sistemi.Controllers
{
    public class HotelInfoController : Controller
    {
        private readonly AppDbContext _db;

        public HotelInfoController(AppDbContext db)
        {
            _db = db;
        }

        // Listeleme (Read)
        public IActionResult Index()
        {
            var list = _db.HotelInfos.ToList();
            return View(list);
        }

        // Detay (Read)
        public IActionResult Details(int id)
        {
            var item = _db.HotelInfos.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Yeni Kayıt (Create) GET
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Kayıt (Create) POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotelInfo model)
        {
            if (ModelState.IsValid)
            {
                _db.HotelInfos.Add(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Düzenle (Update) GET
        public IActionResult Edit(int id)
        {
            var item = _db.HotelInfos.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Düzenle (Update) POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HotelInfo model)
        {
            if (ModelState.IsValid)
            {
                var item = _db.HotelInfos.Find(model.Id);
                if (item == null) return NotFound();

                item.OtelIsmi = model.OtelIsmi;
                item.Slogan = model.Slogan;
                item.Telefon = model.Telefon;
                item.Fax = model.Fax;
                item.Email = model.Email;
                item.Adres = model.Adres;
                item.SosyalInstagram = model.SosyalInstagram;
                item.SosyalFacebook = model.SosyalFacebook;
                item.SosyalTwitter = model.SosyalTwitter;
                item.SosyalYoutube = model.SosyalYoutube;

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Silme (Delete) GET — genelde onay ekranı
        public IActionResult Delete(int id)
        {
            var item = _db.HotelInfos.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Silme (Delete) POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _db.HotelInfos.Find(id);
            if (item == null) return NotFound();

            _db.HotelInfos.Remove(item);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
