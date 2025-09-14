using HotelManagement.DataAccess;            // Dùng để truy cập cơ sở dữ liệu qua Repository
using HotelManagement.Models;                // Chứa các model như TaiKhoan
using Microsoft.AspNetCore.Http;             // Làm việc với phiên làm việc (Session)
using Microsoft.AspNetCore.Mvc;              // Cung cấp các lớp Controller, ActionResult,...
using System.IO;

namespace HotelManagement.Controllers
{
    public class LoginController : Controller
    {
        private IRepository repo; // Interface truy cập dữ liệu (dùng để kiểm tra tài khoản)
        private IHttpContextAccessor httpContextAccessor; // Truy cập HttpContext (để sử dụng Session)

        // Constructor với Dependency Injection
        public LoginController(IRepository repo, IHttpContextAccessor accessor)
        {
            // Gán repository được inject vào field repo
            this.repo = repo;
            this.httpContextAccessor = accessor;
        }

        // Phương thức GET: hiển thị trang đăng nhập
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Trả về view đăng nhập
        }

        // Phương thức POST: xử lý đăng nhập
        [HttpPost]
        public IActionResult Index(TaiKhoan account)
        {
            // Xóa phiên làm việc hiện tại (nếu có)
            httpContextAccessor.HttpContext.Session.Clear();

            // Gọi repository kiểm tra tài khoản trong CSDL
            TaiKhoan check = repo.CheckAccount(account);

            if (check != null) // Nếu đăng nhập đúng
            {
                // Xét loại tài khoản và lưu Session tương ứng
                if (check.LoaiTaiKhoan == "LTK1") // Admin
                {
                    httpContextAccessor.HttpContext.Session.SetString("admin", account.UserName);
                }
                else if (check.LoaiTaiKhoan == "LTK2") // Nhân viên
                {
                    httpContextAccessor.HttpContext.Session.SetString("nhanvien", account.UserName);
                }
                else // Khách hàng hoặc loại khác
                {
                    httpContextAccessor.HttpContext.Session.SetString("UserName", account.UserName);
                }

                // Đăng nhập thành công, chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }

            // Nếu thông tin sai, hiển thị lỗi
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
            return View(); // Hiển thị lại trang đăng nhập
        }

        // Phương thức đăng xuất
        public IActionResult Logout()
        {
            // Xóa toàn bộ thông tin phiên
            httpContextAccessor.HttpContext.Session.Clear();

            // Xóa cụ thể các Session
            httpContextAccessor.HttpContext.Session.Remove("UserName");
            httpContextAccessor.HttpContext.Session.Remove("nhanvien");
            httpContextAccessor.HttpContext.Session.Remove("admin");

            // Quay về trang chủ
            return RedirectToAction("Index", "Home");
        }
    }
}
