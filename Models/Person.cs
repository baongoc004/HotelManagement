using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class Person
    {
        public Person()
        {
            // Khởi tạo collection OrderPhongs như một HashSet rỗng
            // Tránh null reference exception khi truy cập collection
            OrderPhongs = new HashSet<OrderPhong>();
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public string PersonId { get; set; } = null!;
        public string? HoTen { get; set; }
        public int Tuoi { get; set; }
        public int GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Cccd { get; set; }
        public string? Sdt { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } // Danh sách tài khoản liên kết với người này
        public virtual KhachHang? KhachHang { get; set; } // Quan hệ 1-1 nếu người dùng là khách hàng
        public virtual NhanVien? NhanVien { get; set; } // Quan hệ 1-1 nếu người dùng là nhân viên
        public virtual ICollection<OrderPhong> OrderPhongs { get; set; } // Danh sách các đơn đặt phòng đã thực hiện bởi người dùng
    }
}
