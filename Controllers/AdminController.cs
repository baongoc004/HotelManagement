using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace HotelManagement.Controllers
{
    // Controller Admin - xử lý các chức năng quản trị hệ thống khách sạn
    public class AdminController : Controller
    {
        // Khai báo biến repository để truy cập dữ liệu
        private IRepository repo;

        // Constructor - khởi tạo AdminController với dependency injection
        public AdminController(IRepository repo)
        {
            this.repo = repo; // Gán repository được inject vào biến local
        }

        // Action quản lý phòng - hỗ trợ cả GET và POST request
        // Cho phép truy cập bằng HTTP GET (Cho biết phương thức bên dưới chỉ được gọi khi client gửi yêu cầu HTTP GET
        // (tức là khi truy cập URL bằng trình duyệt hoặc gọi API dạng GET).
        // Cho phép truy cập HTTP POST( thường là khi người dùng gửi biểu mẫu (form) hoặc gửi dữ liệu để lưu vào CSDL)
        [HttpGet]
        [HttpPost]
        [AdminOrNhanVienAuthentication]
        public IActionResult QLPhong(string id)
        {
            var loaiphong = repo.getLoaiPhong.ToList();
            var phong = repo.getPhongByLoaiPhong(id).ToList();// Lấy danh sách phòng theo loại phòng (nếu có id)
            var trangthaiphong = repo.getTrangThaiPhong.ToList();
            return View(new LoaiPhongAndPhong { lp = loaiphong, p = phong, ttp = trangthaiphong });
        }

        [Route("[controller]/phong/[action]/{maloaiphong}")] // Custom route: /Admin/phong/removeLoaiPhong/{maloaiphong}
        [AdminAuthentication]
        public IActionResult removeLoaiPhong(string maloaiphong)
        {
            repo.removeLoaiPhong(maloaiphong);
            return RedirectToAction("QLPhong"); // Chuyển hướng về trang quản lý phòng
        }

        [AdminAuthentication]
        [HttpPost]
        public IActionResult themLoaiPhong(string maloaiphong, string tenloaiphong, float gialoaiphong)
        {
            LoaiPhong newLoaiPhong = new LoaiPhong
            {
                MaLoaiPhong = maloaiphong,
                TenLoaiPhong = tenloaiphong,
                GiaPhong = gialoaiphong,
            };
            repo.themLoaiPhong(newLoaiPhong); // Thêm loại phòng vào database
            
            return RedirectToAction("QLPhong"); // Chuyển hướng về trang quản lý phòng

        }

        [AdminAuthentication]
        public IActionResult suaLoaiPhong(string maloaiphong, string tenloaiphong, float gialoaiphong)
        {
            // Gọi repository để cập nhật loại phòng với thông tin mới
            repo.suaLoaiPhong(new LoaiPhong { MaLoaiPhong = maloaiphong, TenLoaiPhong = tenloaiphong, GiaPhong = gialoaiphong });
            return RedirectToAction("QLPhong");
        }

        [AdminAuthentication]
        public IActionResult themPhong(string maphong, string tenphong, string motaphong, string matrangthai, string maloaiphong)
        {
            // Gọi repository để thêm phòng
            repo.themPhong(new Phong
            {
                MaPhong = maphong,
                TenPhong = tenphong,
                MoTaPhong = motaphong,
                MaTrangThai = matrangthai,
                MaLoaiPhong = maloaiphong
            });

            return RedirectToAction("QLPhong");
        }

        [AdminAuthentication]
        [Route("[controller]/phong/[action]/{maphong}")] // Custom route: /Admin/phong/xoaPhong/{maphong} đường dẫn URL
        public IActionResult xoaPhong(string maphong)
        {
            // Gọi repository để xóa phòng theo mã
            repo.xoaPhong(maphong);
            return RedirectToAction("QLPhong");
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult suaPhong(string maphong, string tenphong, string motaphong, string matrangthai, string maloaiphong)
        {
            // Gọi repository để cập nhật thông tin phòng
            repo.suaPhong(new Phong
            {
                MaPhong = maphong,
                TenPhong = tenphong,
                MoTaPhong = motaphong,
                MaTrangThai = matrangthai,
                MaLoaiPhong = maloaiphong
            });
            return RedirectToAction("QLPhong");
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult QLDichVu(bool error = true)
        {
            // Truyền trạng thái error vào ViewBag để hiển thị thông báo
            ViewBag.error = error;
            return View(repo.getDichVus); // Trả về view với danh sách dịch vụ
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult updateDichVu(string madichvu,string tendichvu,float giadichvu)
        {
            DichVu dichvu = new DichVu
            {
                MaDichVu = madichvu,
                TenDichVu = tendichvu,
                GiaDichVu = giadichvu
            };

            repo.updateDichVu(dichvu);
            return RedirectToAction("QLDichVu");
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult xoaDichVu(string madichvu)
        {
            // Gọi repository để xóa dịch vụ và nhận kết quả
            bool check = repo.xoaDichVu(madichvu);
            if (check)
            {
                // Chuyển hướng về trang quản lý dịch vụ khi xóa thành công
                return RedirectToAction("QLDichVu");
            }
            else
            {
                return View("error");
            }
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult themDichVu(string madichvu,string tendichvu,float giadichvu)
        {
            DichVu dichvu = new DichVu
            {
                MaDichVu = madichvu,
                TenDichVu = tendichvu,
                GiaDichVu = giadichvu
            };

            // Gọi repository để thêm dịch vụ và nhận kết quả
            bool check = repo.themDichVu(dichvu);
            if (check)
            {
                // Chuyển hướng với thông báo thành công (error = true)
                return RedirectToAction("QLDichVu", new { error = true });
            }
            else
            {
                return RedirectToAction("QLDichVu",new {error = false});
            }
        }


        [AdminOrNhanVienAuthentication]
        public IActionResult QLHoaDon(DateTime? tuNgay, DateTime? denNgay)
        {
            // Lấy danh sách hóa đơn từ repository
            var danhSachHoaDon = repo.GetHoaDon;

            // Nếu có filter theo ngày
            if (tuNgay.HasValue && denNgay.HasValue)
            {
                // Lọc hóa đơn theo khoảng thời gian
                var hoaDonLoc = danhSachHoaDon.Where(h => h.NgayIn >= tuNgay && h.NgayIn <= denNgay).ToList();

                // Truyền thông tin thống kê qua ViewBag
                ViewBag.TuNgay = tuNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.TongDoanhThu = hoaDonLoc.Sum(h => h.TongTien);
                ViewBag.SoLuongHoaDon = hoaDonLoc.Count();
                ViewBag.TrungBinhHoaDon = hoaDonLoc.Count() > 0 ? hoaDonLoc.Average(h => h.TongTien) : 0;

                // Trả về view với danh sách đã lọc
                return View(hoaDonLoc);
            }

            // Trả về view với danh sách hóa đơn đầy đủ (không filter)
            return View(danhSachHoaDon);
        }


        [AdminOrNhanVienAuthentication]
        public IActionResult chiTietHoaDon(string mahoadon)
        {
            // Lấy chi tiết hóa đơn đầu tiên theo mã hóa đơn
            var s = repo.getChiTietHoaDon(mahoadon).FirstOrDefault();
            // Trả về view với thông tin chi tiết hóa đơn
            return View(s);
        }

        // Action xóa hóa đơn
        //khi xóa hóa đơn thì ta nên xóa order phòng
        [AdminOrNhanVienAuthentication]
        public IActionResult xoaHoadon(string maorder)
        {
            // Xóa order phòng (khi xóa hóa đơn cần xóa order phòng liên quan)
            repo.removeOrderPhong(maorder);
            return RedirectToAction("QLHoaDon");
        }

        // Action quản lý người dùng/khách hàng
        [AdminOrNhanVienAuthentication]
        public IActionResult QLUser()
        {

            return View(repo.getKhachHang);
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult updateThongTinKhachHang(string personid, string hoten, int tuoi, int gioitinh, string sdt)
        {
            Person person = new Person
            {
                PersonId = personid,
                HoTen = hoten,
                Tuoi = tuoi,
                GioiTinh = gioitinh,
                Sdt = sdt
            };
            // Gọi repository để cập nhật thông tin khách hàng
            repo.updateThongTinKhachHang(person);
            return RedirectToAction("QLUser");
        }

        [AdminOrNhanVienAuthentication]
        public IActionResult xoaKhachHang(string personid)
        {
            // Lấy danh sách order phòng của khách hàng
            var orderphongs = repo.getOrderPhongByPerson(personid);
            // Cập nhật trạng thái các phòng về "trống" (MTT1) khi khách hàng bị xóa
            var phongs = orderphongs.Select(o => o.MaPhongNavigation)
                .Select(p =>
                {
                    p.MaTrangThai = "MTT1"; // Chuyển trạng thái về "trống"
                    return p;
                });

            //khi xóa khách hàng xong thì những phòng mà khách hàng order phải xóa theo
            //mà khi order phòng bị xóa thì trạng thái phòng phải chuyển sang thành là "trống" (MTT1)IEnumerable<OrderPhong>           foreach (var orderphong in orderphongs)
            if (phongs != null) repo.updateTrangThaiPhongs(phongs); // Cập nhật trạng thái phòng
            // Xóa khách hàng khỏi database
            repo.removeKhachHang(personid);

            return RedirectToAction("QLUser");
        }

        public IActionResult QLTaiKhoan()
        {
            // Tạo model chứa tất cả thông tin về tài khoản
            LoaiTaiKhoan_TaiKhoan_NhanVien_KhacHang model = new LoaiTaiKhoan_TaiKhoan_NhanVien_KhacHang
            {
                loaiTaiKhoans = repo.getLoaiTaiKhoan,
                taikhoans = repo.getTaiKhoan,
                nhanviens = repo.getTaiKhoanNhanVien,
                khachhangs = repo.getTaiKhoanKhachHang
            };
            return View(model);
        }



        // Action quản lý nhân viên - hỗ trợ cả GET và POST
        // Cho phép HTTP GET (Cho biết phương thức bên dưới chỉ được gọi khi client gửi yêu cầu HTTP GET
        // (tức là khi truy cập URL bằng trình duyệt hoặc gọi API dạng GET).
        [HttpGet]
        [AdminAuthentication]
        // Cho phép HTTP POST( thường là khi người dùng gửi biểu mẫu (form) hoặc gửi dữ liệu để lưu vào CSDL)
        [HttpPost]
        public IActionResult QLNhanVien(string manhanvien = null,
            string hoten = null,
            int tuoi = 0,
            int gioitinh = -1,
            string sdt = null,
            string vaitro = null,
            string username = null,
            string password = null
            , string confirm = null)
        {
            // Tạo model chứa thông tin nhân viên và vai trò
            NhanVien_VaiTro model = new NhanVien_VaiTro
            {
                nhanviens = repo.getTaiKhoanNhanVien, //Danh sách NV
                vaitros = repo.GetVaiTros //DS vai trò
            };

            
            if (manhanvien != null && hoten != null && tuoi > 0 && gioitinh != -1 && sdt != null && vaitro != null && username != null && password != null && confirm != null)
            {
                // Kiểm tra mật khẩu xác nhận
                if (password != confirm)
                {
                    ModelState.AddModelError("", "Mật khẩu không giống");
                }

                // Kiểm tra tên đăng nhập đã tồn tại
                if (repo.checkTonTaiUserName(username))
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại");
                }

                // Kiểm tra mã nhân viên đã tồn tại
                if (repo.checkTonTaiMaNhanVien(manhanvien))
                {
                    ModelState.AddModelError("", "Mã nhân viên đã tồn tại");
                }

                // Tạo object NhanVien mới
                NhanVien nhanvien = new NhanVien
                {
                    NhanVienId = manhanvien,
                    MaVaiTro = vaitro,
                    NgayDuocTuyen = DateTime.Now,
                    NhanVienNavigation = new Person
                    {
                        PersonId = manhanvien,
                        HoTen = hoten,
                        Tuoi = tuoi,
                        GioiTinh = gioitinh,
                        Sdt = sdt
                    },
                };

                // Tạo object TaiKhoan cho nhân viên
                TaiKhoan taikhoan = new TaiKhoan
                {
                    MaTaiKhoan = repo.CreateMaTaiKhoan(), // Tạo mã tài khoản tự động
                    UserName = username,
                    Password = password,
                    LoaiTaiKhoan = "LTK2",
                    PersonId = manhanvien,
                };

                // Thêm nhân viên và tài khoản vào database
                if (repo.addNhanVien(nhanvien) && repo.addTaiKhoanNhanVien(taikhoan))
                {
                    ModelState.AddModelError("", "Tạo thành công");
                }

                else
                {
                    ModelState.AddModelError("", "Tạo thất bại");
                }
            }
            return View(model);
        }

       
        [AdminAuthentication]
        public IActionResult updateThongTinNhanVien(string manhanvien,string hoten,int tuoi,int gioitinh,string sdt,string vaitro)
        {
            // Tạo object Person với thông tin cập nhật
            Person person = new Person
            {
                PersonId = manhanvien,
                HoTen = hoten,
                Tuoi = tuoi,
                GioiTinh = gioitinh,
                Sdt = sdt,
            };
            // Tạo object NhanVien với thông tin cập nhật
            NhanVien nhanvien = new NhanVien
            {
                NhanVienId = manhanvien,
                MaVaiTro = vaitro
            };
            repo.updateThongTinNhanVien(person,nhanvien);

            // Cập nhật loại tài khoản dựa trên vai trò
            if (vaitro == "MVT1") // Nếu là vai trò Admin
            {
                repo.updateLoaiTaiKhoanOfPerson(manhanvien, "LTK1"); // Cập nhật loại tài khoản Admin
            }
            else // Nếu là vai trò khác
            {
                repo.updateLoaiTaiKhoanOfPerson(manhanvien, "LTK2"); // Cập nhật loại tài khoản Nhân viên
            }
            return RedirectToAction("QLNhanVien");
        }

        [AdminAuthentication]
        public IActionResult xoaNhanVien(string manhanvien)
        {
            // Gọi repository để xóa nhân viên
            bool check = repo.removeNhanVien(manhanvien);

            return RedirectToAction("QLNhanVien");
        }

        [AdminAuthentication]
        public IActionResult updateLoaiTaiKhoan(string maloaitaikhoan,string tenloaitaikhoan)
        {
            LoaiTaiKhoan loaitaikhoan = new LoaiTaiKhoan
            {
                MaLoaiTaiKhoan = maloaitaikhoan,
                TenLoai = tenloaitaikhoan
            };
            repo.updateLoaiTaiKhoan(loaitaikhoan);
            return RedirectToAction("QLTaiKhoan");
        }

        [AdminAuthentication]
        public IActionResult updateTaiKhoanNhanVien(string mataikhoan,string username, string password)
        {
            // Gọi repository để cập nhật tài khoản với thông tin mới
            repo.updateTaiKhoan(mataikhoan, username, password);    
            return RedirectToAction("QLTaiKhoan");
        }
        [AdminAuthentication]
        public IActionResult updateTaiKhoanKhachHang(string mataikhoan, string username, string password)
        {
            repo.updateTaiKhoan(mataikhoan, username, password);
            return RedirectToAction("QLTaiKhoan");
        }

    }

    // Model class chứa thông tin loại phòng và phòng
    public class LoaiPhongAndPhong
    {
        public List<LoaiPhong> lp { get; set; } // Danh sách loại phòng
        public List<Phong> p { get; set; }
        public List<TrangThaiPhong> ttp { get; set; }
    }


    public class LoaiTaiKhoan_TaiKhoan_NhanVien_KhacHang
    {
        public IEnumerable<LoaiTaiKhoan> loaiTaiKhoans { get; set; }
        public IEnumerable<TaiKhoan> taikhoans { get; set; }
        public IEnumerable<NhanVien> nhanviens { get; set; }
        public IEnumerable<KhachHang> khachhangs { get; set; }
    }

    public class NhanVien_VaiTro
    {
        public IEnumerable<NhanVien> nhanviens { get; set; }
        public IEnumerable<VaiTro> vaitros { get; set; }
    }
}
