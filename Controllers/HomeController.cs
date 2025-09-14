using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HotelManagement.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repo;
        // Hàm khởi tạo, nhận đối tượng repository qua Dependency Injection
        public HomeController(IRepository repository)
        {
            this.repo = repository; // Lưu lại để dùng trong controller
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index() // Xử lý khi người dùng truy cập trang chủ (GET hoặc POST)
        {
            return View(); // Trả về giao diện (View) tương ứng
        }
    }
}
