using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;

namespace Otel_Sistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                ReservationForm = new ReservationViewModel(),
                AvailableRooms = null
            };


            return View(model);
        }

        //   [HttpPost]
        //   public IActionResult CheckAvailability(HomeViewModel model)
        //   {
        //       // ModelState kontrolü istersen bunu açabilirsin
        //       // if (!ModelState.IsValid)
        //       // {
        //       //     var hotelInfo = _context.HotelInfos.FirstOrDefault();
        //       //     return View("Index", new HomeViewModel
        //       //     {
        //       //         ReservationForm = model.ReservationForm,
        //       //         Hotel = hotelInfo,
        //       //         AvailableRooms = new List<Room>(),
        //       //         GunSayisi = 0
        //       //     });
        //       // }

        //       var form = model.ReservationForm;
        //       var girisTarihi = form.GirisTarihi.Date;
        //       var cikisTarihi = form.CikisTarihi.Date;
        //       var gunSayisi = (cikisTarihi - girisTarihi).Days;

        //       // Tüm uygun odaları al
        //       var uygunOdalar = _context.Rooms
        //.Include(r => r.RoomServices)
        //.Where(o => !_context.Reservations
        //    .Any(r =>
        //        r.RoomId == o.Id &&
        //        !r.IptalEdildi &&
        //        girisTarihi < r.CikisTarihi &&
        //        cikisTarihi > r.GirisTarihi
        //    )
        //)
        //.ToList();


        //       // Her oda türünden sadece bir tanesini al
        //       var benzersizOdaTurleri = uygunOdalar
        //           .GroupBy(r => r.OdaTuru)
        //           .Select(g => g.First())
        //           .ToList();

        //       // Otel bilgisi
        //       var hotel = _context.HotelInfos.FirstOrDefault();

        //       // ViewModel oluştur
        //       var homeViewModel = new HomeViewModel
        //       {
        //           ReservationForm = form,
        //           Hotel = hotel,
        //           AvailableRooms = benzersizOdaTurleri,
        //           GunSayisi = gunSayisi
        //       };

        //       return View("CheckAvailability", homeViewModel);
        //   }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CheckAvailability(HomeViewModel model)
        //{
        //    var form = model.ReservationForm;
        //    if (form == null || form.GirisTarihi == null || form.CikisTarihi == null || form.GirisTarihi >= form.CikisTarihi)
        //    {
        //        TempData["Error"] = "Geçerli bir giriş ve çıkış tarihi giriniz.";
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var girisTarihi = form.GirisTarihi.Value.Date;
        //    var cikisTarihi = form.CikisTarihi.Value.Date;
        //    var gunSayisi = (cikisTarihi - girisTarihi).Days;

        //    // 1. Uygun odaları getir
        //    var uygunOdalar = _context.Rooms
        //        .Where(o => !_context.Reservations.Any(r =>
        //            r.RoomId == o.Id &&
        //            !r.IptalEdildi &&
        //            girisTarihi < r.CikisTarihi &&
        //            cikisTarihi > r.GirisTarihi
        //        ))
        //        .ToList();

        //    // 2. Her oda türünden sadece bir tanesini al
        //    var benzersizOdalar = uygunOdalar
        //        .GroupBy(r => r.OdaTuru)
        //        .Select(g => g.First())
        //        .ToList();

        //    // 3. Hizmetleri ata
        //    foreach (var oda in benzersizOdalar)
        //    {
        //        var hizmetString = _context.RoomServices
        //            .Where(rs => rs.OdaTuru == oda.OdaTuru)
        //            .Select(rs => rs.Hizmet)
        //            .FirstOrDefault();

        //        var hizmetListesi = (hizmetString ?? "")
        //            .Split(',', StringSplitOptions.RemoveEmptyEntries)
        //            .Select(h => h.Trim())
        //            .ToList();

        //        oda.RoomServices = hizmetListesi
        //            .Select(h => new RoomService { Hizmet = h })
        //            .ToList();
        //    }

        //    // 4. ViewModel oluştur ve dön
        //    var hotel = _context.HotelInfos.FirstOrDefault();

        //    var homeViewModel = new HomeViewModel
        //    {
        //        ReservationForm = form,
        //        Hotel = hotel,
        //        AvailableRooms = benzersizOdalar,
        //        GunSayisi = gunSayisi
        //    };

        //    return View("CheckAvailability", homeViewModel);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckAvailability(HomeViewModel model)
        {
            var form = model.ReservationForm;
            if (form == null || form.GirisTarihi == null || form.CikisTarihi == null || form.GirisTarihi >= form.CikisTarihi)
            {
                TempData["Error"] = "Geçerli bir giriş ve çıkış tarihi giriniz.";
                return RedirectToAction("Index", "Home");
            }

            var girisTarihi = form.GirisTarihi.Value.Date;
            var cikisTarihi = form.CikisTarihi.Value.Date;
            var gunSayisi = (cikisTarihi - girisTarihi).Days;

            var toplamKisi = form.YetiskinSayisi + form.CocukSayisi;

            // Uygun odaları getir + kişi sayısına göre filtrele
            var uygunOdalar = _context.Rooms
    .Where(o =>
        !_context.Reservations.Any(r =>
            r.RoomId == o.Id &&
            !r.IptalEdildi &&
            girisTarihi < r.CikisTarihi &&
            cikisTarihi > r.GirisTarihi
        )
        &&
        (
            toplamKisi <= o.Kapasite ||
            (o.IlaveYatak && toplamKisi <= o.Kapasite + 1)
        )
        &&
        (
            form.CocukSayisi == 0 || o.CocukKabul  // çocuk varsa sadece çocuk kabul edenler gelsin
        )
    )
    .ToList();


            var benzersizOdalar = uygunOdalar
                .GroupBy(r => r.OdaTuru)
                .Select(g => g.First())
                .ToList();

            foreach (var oda in benzersizOdalar)
            {
                var hizmetString = _context.RoomServices
                    .Where(rs => rs.OdaTuru == oda.OdaTuru)
                    .Select(rs => rs.Hizmet)
                    .FirstOrDefault();

                var hizmetListesi = (hizmetString ?? "")
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(h => h.Trim())
                    .ToList();

                oda.RoomServices = hizmetListesi
                    .Select(h => new RoomService { Hizmet = h })
                    .ToList();
            }

            var hotel = _context.HotelInfos.FirstOrDefault();

            var homeViewModel = new HomeViewModel
            {
                ReservationForm = form,
                Hotel = hotel,
                AvailableRooms = benzersizOdalar,
                GunSayisi = gunSayisi
            };

            return View("CheckAvailability", homeViewModel);
        }







        public IActionResult Rooms()
        {
            var rooms = _context.Rooms.ToList();
            var roomServices = _context.RoomServices.ToList();

            var viewModel = rooms.Select(room => new RoomWithServicesViewModel
            {
                Room = room,
                Hizmetler = roomServices
        .Where(rs => rs.OdaTuru == room.OdaTuru)  // Oda türüne göre filtrele
        .SelectMany(rs => rs.Hizmet.Split(','))   // Her hizmeti virgülle parçala, hepsini tek listeye indir
        .Select(h => h.Trim())                     // Her bir hizmeti temizle
        .Distinct()                               // Aynı hizmet tekrarını önle
        .ToList()
            }).ToList();



            return View(viewModel);
        }


        public IActionResult AboutUs()
        {
            var model = new AboutViewModel
            {
                AboutItems = _context.AboutItems.ToList(),
                AboutText = _context.HomeTexts.FirstOrDefault(x => x.Sayfa == "hakkimizda")
            };

            return View(model); // View => Views/Home/AboutUs.cshtml
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var hotelInfo = _context.HotelInfos.FirstOrDefault(); // örnek: sadece ilkini alıyoruz
            var model = new ContactViewModel
            {
                HotelInfo = hotelInfo
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ContactMessage.Tarih = DateTime.Now;
                _context.ContactMessages.Add(model.ContactMessage);
                _context.SaveChanges();

                TempData["Message"] = "Mesajınız başarıyla gönderildi.";
                return RedirectToAction("Contact");
            }

            model.HotelInfo = _context.HotelInfos.FirstOrDefault();
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
