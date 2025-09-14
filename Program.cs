using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // T?o builder cho c?u hình ng dùng web
            var builder = WebApplication.CreateBuilder(args);

            // Thêm dich v? Session, qu?n lý phiên làm vi?c c?a ng??i dùng
            builder.Services.AddSession();

            // Thêm cache phân tán trong b? nh? l?u tr? session data
            builder.Services.AddDistributedMemoryCache();

            // ??ng ký IHttpContextAccessor nh? Singleton ?? truy c?p HttpContext t? b?t k? ?âu trong ?ng d?ng
            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            // Add services to the container.
            // Thêm d?ch v? Controllers và Views (MVC pattern)
            builder.Services.AddControllersWithViews();

            // C?u hình Entity Framework DbContext
            builder.Services.AddDbContext<HotelContext>(option =>
            {
                option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });

            // ??ng ký Repository pattern v?i Dependency Injection
            // Khi inject IRepository, s? s? d?ng l?p Repository làm hi?n th?c
            builder.Services.AddScoped<IRepository, Repository>();

            // Build ?ng d?ng t? các c?u hình ?ã thi?t l?p
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Ki?m tra n?u KHÔNG ph?i môi tr??ng phát tri?n (development)
            if (!app.Environment.IsDevelopment())
            {
                // S? d?ng trình x? lý l?i m?c ??nh khi x?y ra exception
                app.UseExceptionHandler("/Home/Error");
            }
            // Cho phép dùng các file t?nh (CSS, JS, images, etc.)
            app.UseStaticFiles();

            // Kích ho?t routing controller/action
            app.UseRouting();

            app.UseAuthentication();  // Kích ho?t middleware xác th?c (authentication)
            app.UseAuthorization();   // Kích ho?t middleware phân quy?n (authorization)

            app.UseSession();        // Kích ho?t middleware session

            // C?u hình route cho MVC
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}",     // Pattern URL: controller/action/id (id là optional)
                defaults: new {controller= "Home",action = "Index"});

           
            app.Run();
        }
    }
}