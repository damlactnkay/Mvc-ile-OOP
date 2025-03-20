using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Veritabanı bağlantısını al
var connectionString = builder.Configuration.GetConnectionString("ContextConnection")
    ?? throw new InvalidOperationException("Connection string 'ContextConnection' not found.");

// 2️⃣ DbContext ve Identity Servislerini ekle
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;  // Rakam zorunluluğunu kaldır
    options.Password.RequireLowercase = false; // Küçük harf zorunluluğunu kaldır
    options.Password.RequireUppercase = false; // Büyük harf zorunluluğunu kaldır
    options.Password.RequireNonAlphanumeric = false; // Özel karakter zorunluluğunu kaldır
    options.Password.RequiredLength = 4; // Minimum şifre uzunluğu
})
.AddEntityFrameworkStores<Context>();


// 3️⃣ MVC Controller ve Razor Pages servisini ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4️⃣ Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 5️⃣ HTTPS yönlendirmesi, statik dosyalar ve kimlik doğrulama
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik doğrulama aktif
app.UseAuthorization();

// 6️⃣ Varsayılan yönlendirme
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 7️⃣ Uygulamayı başlat
app.Run();

