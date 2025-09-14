using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace HotelManagement.Controllers
{
    public class SignupController : Controller
    {

        private IRepository repo;
        public SignupController(IRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet] // Attribute chỉ định method này chỉ xử lý HTTP GET request
        public IActionResult Index() // Method action Index() GET - hiển thị form đăng ký
        {
            // Tạo một đối tượng mới của PersonAndAccount để truyền vào view
            // PersonAndAccount là model chứa thông tin cá nhân và tài khoản
            PersonAndAccount p = new PersonAndAccount();
            return View(p);
        }

        [HttpPost]
        // Method action Index() POST - xử lý dữ liệu đăng ký được submit từ form
        // Tham số paa là model PersonAndAccount được bind từ form data
        public IActionResult Index(PersonAndAccount paa)
        {
            if (paa.confirm != paa.a.Password)
            {
                ModelState.AddModelError("", "Mật khẩu không giống nhau");
            }
            else // Nếu mật khẩu xác nhận đúng
            {
                // Gọi repository để tạo tài khoản mới
                bool taoTaiKhoan = repo.CreateAccount(new TaiKhoan
                {
                    MaTaiKhoan = repo.CreateMaTaiKhoan(),
                    UserName = paa.a.UserName, // Gán username từ form data
                    Password = paa.a.Password, // Gán password từ form data
                    LoaiTaiKhoan = "LTK3", // Gán loại tài khoản là "LTK3" (có thể là loại tài khoản khách hàng)
                    Person = paa.p          // Gán thông tin cá nhân Person từ form data
                });
                if (taoTaiKhoan == false) // Kiểm tra kết quả tạo tài khoản
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                }
                else ModelState.AddModelError("", "Tạo tài khoản thành công");
            }
            return View();
        }
    }

    public class PersonAndAccount // Class model để chứa dữ liệu cho form đăng ký
    {
        public Person p { get; set; } // Property chứa thông tin cá nhân (Person)
        public TaiKhoan a { get; set; } // Property chứa thông tin tài khoản (TaiKhoan)
        public string confirm { get; set; } // Property chứa mật khẩu xác nhận (để so sánh với mật khẩu gốc)
    }
}
