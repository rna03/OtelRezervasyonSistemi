using Microsoft.EntityFrameworkCore;
using Otel_Sistemi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// 🟡 Authentication (Giriş Çıkış)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options =>
   {
       options.LoginPath = "/Account/Login";
       options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum süresi 30 dk
       options.SlidingExpiration = false; // Her istekte süre uzamasın
       options.Cookie.IsEssential = true;
   });

// 🟢 Session Ayarı
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session da 30 dk sonra biter
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddHttpContextAccessor(); // Session ve kullanıcı bilgileri için

var app = builder.Build();

// Middleware Sırası Önemli!
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✨ Session ve Auth middleware'leri
app.UseSession();
app.UseAuthentication(); // ➕ Giriş kontrolü
app.UseAuthorization();


app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{action=Index}/{id?}",
    defaults: new { controller = "AdminRedirect" });



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
