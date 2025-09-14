using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HotelManagement.Controllers
{
    // Controller xử lý lịch sử đặt phòng của người dùng
    public class BookingHistoryOfUser : Controller
    {
        // Khai báo biến repository để truy cập dữ liệu
        private IRepository repo;
        // Khai báo biến accessor để truy cập HttpContext
        private IHttpContextAccessor accessor;

        // Constructor - khởi tạo BookingHistoryOfUser với dependency injection( 
        // cung cấp các đối tượng mà một lớp (class) cần thay vì để lớp đó tự tạo ra chúng)
        public BookingHistoryOfUser(IRepository repo, IHttpContextAccessor accessor)
        {
            this.repo = repo; // Gán repository được inject vào biến local
            this.accessor = accessor; // Gán HttpContextAccessor được inject vào biến local
        }

        // Action chính hiển thị lịch sử đặt phòng
        [Authentication]
        public IActionResult Index()
        {
            //đầu tiên lấy Person
            // Lấy tên đăng nhập từ Session
            string userName = accessor.HttpContext.Session.GetString("UserName");
            // Tìm thông tin Person dựa trên tên đăng nhập
            Person p = repo.getPersonByUserName(userName);
            //từ person lấy ra OrderPhong của person đó
            // Lấy danh sách các đơn đặt phòng của người dùng này
            IEnumerable<OrderPhong> oderPhongs = repo.getOrderPhongByPerson(p.PersonId);
            return View(oderPhongs);
        }

        // Action hủy đơn đặt phòng
        [Authentication] // Yêu cầu người dùng phải đăng nhập
        public IActionResult removeOrder(string maorder, string maphong)
        {
            repo.removeOrderPhong(maorder);
            //sau khi xóa order phòng xong thì cập nhật lại trạng thái là trống
            // Cập nhật trạng thái phòng về "trống" (MTT1) sau khi hủy đặt phòng
            repo.updateTrangThaiPhong(maphong, "MTT1");
            // Chuyển hướng về trang Index để hiển thị danh sách cập nhật
            return RedirectToAction("Index");
        }

        // Action hiển thị modal thanh toán - trả về PartialView
        [Authentication]
        public IActionResult PaymentModal(string maorder)
        {
            // Lấy thông tin đơn đặt phòng theo mã order
            OrderPhong order = repo.getOrderPhongByMa(maorder);
            return PartialView("_PaymentModal", order);
        }

        // Action xử lý thanh toán bằng chuyển khoản
        [Authentication]
        [HttpPost]
        public IActionResult PaymentByTransfer(string maorder)
        {
            try
            {
                // Lấy thông tin order trước để kiểm tra
                OrderPhong order = repo.getOrderPhongByMa(maorder);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn đặt phòng!";
                    return RedirectToAction("Index");
                }

                // Cập nhật trạng thái thanh toán của order
                bool result = repo.updatePaymentStatus(maorder, "ChuyenKhoan", "DaThanhToan");

                if (result)
                {
                    // Cập nhật trạng thái order phòng thành đã thanh toán (1)
                    repo.updateTrangThaiOrderPhong(maorder);

                    // Cập nhật trạng thái phòng thành "đã đặt" - có thể dùng mã phù hợp với hệ thống
                    repo.updateTrangThaiPhong(order.MaPhong, "MTT2");

                    TempData["SuccessMessage"] = "Thanh toán bằng chuyển khoản thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình thanh toán!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi hệ thống: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        // Action xử lý thanh toán bằng tiền mặt
        [Authentication]
        [HttpPost]
        public IActionResult PaymentByCash(string maorder)
        {
            try
            {
                // Lấy thông tin order trước để kiểm tra
                OrderPhong order = repo.getOrderPhongByMa(maorder);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn đặt phòng!";
                    return RedirectToAction("Index");
                }

                // Cập nhật trạng thái thanh toán của order
                bool result = repo.updatePaymentStatus(maorder, "TienMat", "DaThanhToan");

                if (result)
                {
                    // Cập nhật trạng thái order phòng thành đã thanh toán (1)
                    repo.updateTrangThaiOrderPhong(maorder);

                    // Cập nhật trạng thái phòng thành "đã đặt" - có thể dùng mã phù hợp với hệ thống
                    repo.updateTrangThaiPhong(order.MaPhong, "MTT2");

                    TempData["SuccessMessage"] = "Đã xác nhận thanh toán bằng tiền mặt!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra trong quá trình xác nhận thanh toán!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi hệ thống: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}