using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Otel_Sistemi.Data;
using Otel_Sistemi.Models;
using System.Security.Claims;

namespace Otel_Sistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u =>
                u.TcKimlikNo == model.TcKimlikNo && u.Sifre == model.Sifre);

            if (user == null)
            {
                ViewBag.Hata = "TC Kimlik No veya şifre hatalı.";
                return View(model);
            }

            // Kimlik bilgilerini oluştur
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Ad + " " + user.Soyad),
        new Claim(ClaimTypes.Role, user.Rol)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Oturumu başlat
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Session'a da yaz (isteğe bağlı)
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserAdSoyad", user.Ad + " " + user.Soyad);

            // Rezerve işleminden gelen varsa devam et
            if (TempData["RoomId"] != null && TempData["GirisTarihi"] != null && TempData["CikisTarihi"] != null)
            {
                TempData.Keep("RoomId");
                TempData.Keep("GirisTarihi");
                TempData.Keep("CikisTarihi");

                return RedirectToAction("ContinueAfterLogin", "Reservation");
            }

            // Eğer returnUrl varsa ona git
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Admin ise admin paneline yönlendir
            if (user.Rol == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            // Diğer kullanıcılar anasayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); // Session temizle
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Cookie temizle
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                Sifre = model.Sifre,
                TcKimlikNo = model.TcKimlikNo,
                Rol = "musteri" // otomatik müşteri atanır
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }


    }
}
