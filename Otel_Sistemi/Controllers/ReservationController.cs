using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;
using System.Security.Claims;

namespace Otel_Sistemi.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult MyReservations()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var reservations = _context.Reservations
                .Include(r => r.Room)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.GirisTarihi)
                .ToList();

            return View(reservations); // Views/Reservation/MyReservations.cshtml
        }

        [HttpPost]
        public IActionResult IptalEt(int id)
        {
            var rezervasyon = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (rezervasyon != null)
            {
                rezervasyon.IptalEdildi = true;
                _context.SaveChanges();
            }

            return RedirectToAction("MyReservations");
        }
        [HttpGet]
        public IActionResult RezerveEt(int roomId, DateTime girisTarihi, DateTime cikisTarihi)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["RoomId"] = roomId;
                TempData["GirisTarihi"] = girisTarihi.ToString("yyyy-MM-dd");
                TempData["CikisTarihi"] = cikisTarihi.ToString("yyyy-MM-dd");
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var rezervasyon = new Reservation
            {
                RoomId = roomId,
                GirisTarihi = girisTarihi,
                CikisTarihi = cikisTarihi,
                UserId = userId,
                IptalEdildi = false
            };

            _context.Reservations.Add(rezervasyon);
            _context.SaveChanges();

            return RedirectToAction("MyReservations");
        }

      
    
        [HttpPost]
        [Authorize]
        public IActionResult RezerveEtPost(string odaTuru, DateTime girisTarihi, DateTime cikisTarihi)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // 1️⃣ Bu oda türünden uygun odaları bul
            var uygunOda = _context.Rooms
                .Where(o =>
                    o.OdaTuru == odaTuru &&
                    !_context.Reservations.Any(r =>
                        r.RoomId == o.Id &&
                        !r.IptalEdildi &&
                        girisTarihi < r.CikisTarihi &&
                        cikisTarihi > r.GirisTarihi
                    )
                )
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefault();

            // 2️⃣ Eğer hiçbir uygun oda kalmadıysa uyar
            if (uygunOda == null)
            {
                TempData["Error"] = "Seçilen oda türü için uygun başka bir oda kalmamıştır.";
                return RedirectToAction("Index", "Home");
            }

            // 3️⃣ Yeni rezervasyonu oluştur
            int gunSayisi = (cikisTarihi - girisTarihi).Days;
            decimal toplamTutar = uygunOda.Fiyat * gunSayisi;

            var rezervasyon = new Reservation
            {
                RoomId = uygunOda.Id,
                GirisTarihi = girisTarihi,
                CikisTarihi = cikisTarihi,
                UserId = userId,
                IptalEdildi = false,
                ToplamTutar = toplamTutar
            };

            _context.Reservations.Add(rezervasyon);
            _context.SaveChanges();

            return RedirectToAction("MyReservations");
        }




        // Login sonrası HomeController gibi bir yerde olabilir
        public IActionResult ContinueAfterLogin()
        {
            if (TempData["RoomId"] != null)
            {
                int roomId = int.Parse(TempData["RoomId"].ToString());
                DateTime girisTarihi = DateTime.Parse(TempData["GirisTarihi"].ToString());
                DateTime cikisTarihi = DateTime.Parse(TempData["CikisTarihi"].ToString());

                return RedirectToAction("RezerveEt", "Reservation", new { roomId, girisTarihi, cikisTarihi });
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
