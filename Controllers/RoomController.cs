using Microsoft.AspNetCore.Authorization; // Import namespace để sử dụng Authorization attribute cho phân quyền
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models.Authentication;
using HotelManagement.DataAccess;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private IRepository repo;
        IHttpContextAccessor accessor;
        public RoomController(IRepository repo, IHttpContextAccessor accessor)
        {
            // Gán repository được inject vào field repo
            this.repo = repo;
            // Gán httpContextAccessor được inject vào field accessor
            this.accessor = accessor;
        }

        // Khởi tạo một instance của LoaiPhongPhongTrangThaiPhong để chứa dữ liệu truyền ra view
        LoaiPhongPhongTrangThaiPhong treetable = new LoaiPhongPhongTrangThaiPhong();
        // Attribute chỉ định method này xử lý cả HTTP GET và POST request
        [HttpGet]
        [HttpPost]
        public IActionResult Index(string loaiphong = null, string trangthaiphong = null, bool error = true)
        {
            // Nếu cả 2 tham số lọc đều null thì lấy tất cả phòng
            if (loaiphong == null && trangthaiphong == null) treetable.phongs = repo.getPhongByLoaiPhong(null);
            // Nếu chỉ có tham số trạng thái phòng thì lọc theo trạng thái
            else if (loaiphong == null) treetable.phongs = repo.getPhongByMaTrangThai(trangthaiphong);
            // Nếu chỉ có tham số loại phòng thì lọc theo loại phòng
            else if (trangthaiphong == null) treetable.phongs = repo.getPhongByLoaiPhong(loaiphong);

            // Lấy danh sách tất cả trạng thái phòng từ database
            treetable.trangthaiphongs = repo.getTrangThaiPhong;
            treetable.loaiphongs = repo.getLoaiPhong;
            treetable.dichvus = repo.getDichvu;
            // Gán trạng thái error để hiển thị thông báo
            treetable.error = error;
            // Kiểm tra nếu có user đã đăng nhập (session UserName tồn tại)
            if (accessor.HttpContext.Session.GetString("UserName") != null)
            {
                // Lấy thông tin người dùng từ database theo username trong session
                treetable.Person = repo.getPersonByUserName(accessor.HttpContext.Session.GetString("UserName"));
            }
            // Trả về view với dữ liệu treetable
            return View(treetable);
        }

        // Attribute tùy chỉnh để kiểm tra authentication (người dùng phải đăng nhập)
        [Authentication]
        // Method action đặt phòng và dịch vụ - nhận nhiều tham số từ form
        public IActionResult datPhongVaDichVu(string hoten,
            int tuoi,
            int gioitinh,
            string cccd,
            string sdt,
            DateTime ngayden,
            DateTime ngaydi,
            string maphong,
            string selectedServiceIds, // Chuỗi ID các dịch vụ được chọn (phân tách bằng dấu phẩy)
            string servicePrice,       // Chuỗi giá các dịch vụ (phân tách bằng dấu phẩy)
            string selectedQuantities) // Chuỗi số lượng các dịch vụ (phân tách bằng dấu phẩy)
        {
            // Kiểm tra nếu các thông tin bắt buộc không được điền thì redirect về trang Index với error = false
            if (tuoi == 0 && gioitinh == 0 && cccd == null && sdt == null && ngayden == DateTime.MinValue && ngaydi == DateTime.MinValue)
            {
                 return RedirectToAction("Index", "Room",new {error = false});
            }
            // Khởi tạo đối tượng Person để lưu thông tin khách hàng
            Person person = new Person();
            // Nếu có user đã đăng nhập (session UserName tồn tại)
            if (accessor.HttpContext.Session.GetString("UserName") != null)
            {
                // Lấy thông tin person từ database theo username
                person = repo.getPersonByUserName(accessor.HttpContext.Session.GetString("UserName"));
            }
            else
            {
                person = new Person
                {
                    PersonId = cccd,
                    HoTen = hoten,
                    Tuoi = tuoi,
                    GioiTinh = gioitinh,
                    Sdt = sdt
                };
            }

            // Tạo mã order phòng mới từ repository
            string maorderphong = repo.createOrderPhongId();
            OrderPhong orderphong = new OrderPhong // Tạo đối tượng OrderPhong với thông tin đặt phòng
            {
                MaOrderPhong = maorderphong,
                NgayDen = ngayden,
                NgayDi = ngaydi,
                PersonId = cccd,
                MaPhong = maphong,
                Person = person,
                TrangThaiThanhToan = 0,
                MaPhongNavigation = repo.getPhongByMaPhong(maphong)
            };


            //đầu tiên add order phòng
            // Thêm order phòng vào database
            repo.addOrderPhong(orderphong);

            //tiếp theo add order phòng và danh sách dịch vụ của order phòng đó
            // Kiểm tra nếu có dịch vụ được chọn
            if (selectedServiceIds != null && selectedQuantities != null && servicePrice != null)
            {
                // Chuyển chuỗi ID dịch vụ thành List<string> (phân tách bằng dấu phẩy)
                List<string> madichvu = selectedServiceIds.Split(',').ToList();
                // Chuyển chuỗi số lượng thành List<int>
                List<int> soLuongMoiDichVu = selectedQuantities.Split(",").Select(int.Parse).ToList();
                // Chuyển chuỗi giá thành List<float>
                List<float> giaMoiDichVu = servicePrice.Split(",").Select(float.Parse).ToList();

                // Tạo danh sách OrderPhongDichVu để lưu các dịch vụ của order
                List<OrderPhongDichVu> orderphongdichvu = new List<OrderPhongDichVu>();
                // Lặp qua từng dịch vụ để tạo OrderPhongDichVu
                for (int i = 0; i < madichvu.Count(); i++)
                {
                    orderphongdichvu.Add(new OrderPhongDichVu
                    {
                        MaOrderPhong = maorderphong,
                        MaDichVu = madichvu[i],
                        SoLuong = soLuongMoiDichVu[i],
                        DonGia = giaMoiDichVu[i]
                    });
                }

                repo.addOrderPhongDichVu(orderphongdichvu);
            }


            //cuối cùng update trạng thái phòng là đăng thuê

            //người đăng kí phòng là user thì mã trạng thái phòng là đặt trước,nếu là admin thì trạng thái đang thuê
            // Nếu có user đăng nhập (khách hàng đặt phòng)
            if (accessor.HttpContext.Session.GetString("UserName") != null)
            {
                // Cập nhật trạng thái phòng thành "đặt trước" (MTT3)
                repo.updateTrangThaiPhong(maphong, "MTT3");
            }
            // Nếu admin đặt phòng trực tiếp
            else repo.updateTrangThaiPhong(maphong, "MTT2"); // Cập nhật trạng thái phòng thành "đang thuê" (MTT2)
            // Redirect về trang Index với error = true để hiển thị thông báo thành công
            return RedirectToAction("Index", "Room", new { error = true });
        }


        [AdminOrNhanVienAuthentication]
        [Route("[controller]/[action]/{maphong}/{successOrFail?}")]
        public IActionResult thanhToan(string maphong, string successOrFail = "0")
        {
            //TrangThaiThanhToan == 0 : chưa thanh toán
            //TrangThaiThanhToan == 1: đã thanh toán
            // Lấy order phòng theo mã phòng và trạng thái chưa thanh toán (0)
            OrderPhong order = repo.getOrderPhongByMaPhong(maphong).FirstOrDefault(od => od.TrangThaiThanhToan == 0);
            return View("thanhToan", order); // Trả về view "thanhToan" với dữ liệu order
        }

        [AdminOrNhanVienAuthentication]
        [Route("[controller]/[action]/maorder")]
        public IActionResult addHoadon(string maorder, string tongtien, string maphong)
        {
            HoaDon hd = new HoaDon
            {
                MaHoaDon = repo.createHoaDonId(),
                NgayIn = DateTime.Now,
                TongTien = float.Parse(tongtien),
                MaOrderPhong = maorder,
            };

            // Thêm hóa đơn vào database và kiểm tra kết quả
            bool checkHoaDonOrderPHong = repo.addHoaDon(hd);
            if (checkHoaDonOrderPHong) // Nếu thêm hóa đơn thành công
            {
                // Cập nhật trạng thái phòng về "trống" (MTT1) sau khi thanh toán
                repo.updateTrangThaiPhong(maphong, "MTT1");
                // Cập nhật trạng thái thanh toán của order thành đã thanh toán
                repo.updateTrangThaiOrderPhong(maorder);

                return View("ThanhToanThanhCong");
            }
            else
            {
                // Redirect về trang thanh toán với mã phòng để thử lại
                return RedirectToAction("thanhToan", "Room", new { maphong = maphong });
            }

        }

        [AdminOrNhanVienAuthentication]
        public IActionResult xacNhanDatPhong(string maphong)
        {
            // Cập nhật trạng thái phòng thành "đang thuê" (MTT2)
            repo.updateTrangThaiPhong(maphong, "MTT2");
            return RedirectToAction("Index"); // Redirect về trang Index
        }


    }

    public class LoaiPhongPhongTrangThaiPhong
    {
        public LoaiPhongPhongTrangThaiPhong() // Constructor mặc định
        {

        }
        public IEnumerable<LoaiPhong> loaiphongs { get; set; } // Property chứa danh sách loại phòng
        public IEnumerable<Phong> phongs { get; set; }
        public IEnumerable<TrangThaiPhong> trangthaiphongs { get; set; }
        public IEnumerable<DichVu> dichvus { get; set; }
        public Person Person { get; set; } = null; // Property chứa thông tin người dùng hiện tại (mặc định null)
        public bool error { get; set; } = true; // Property để kiểm soát hiển thị thông báo lỗi (mặc định true)
    }

}
