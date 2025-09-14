using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class OrderPhong
    {
        public OrderPhong()
        {
            HoaDons = new HashSet<HoaDon>();
            OrderPhongDichVus = new HashSet<OrderPhongDichVu>();
        }

        public string MaOrderPhong { get; set; } = null!;
        public DateTime? NgayDen { get; set; }
        public DateTime? NgayDi { get; set; }
        public string PersonId { get; set; } = null!;
        public string MaPhong { get; set; } = null!;
        public int TrangThaiThanhToan { get; set; }  = 0; // Trạng thái thanh toán: 0 = chưa thanh toán, 1 = đã thanh toán

        public virtual Phong MaPhongNavigation { get; set; } = null!; // Thuộc tính điều hướng đến phòng được đặt
        public virtual Person Person { get; set; } = null!; // Thuộc tính điều hướng đến người đặt phòng (Person)
        public virtual ICollection<HoaDon> HoaDons { get; set; } // Danh sách hóa đơn liên quan đến đơn đặt phòng này
        public virtual ICollection<OrderPhongDichVu> OrderPhongDichVus { get; set; }  // Danh sách dịch vụ được đặt kèm theo trong đơn này

        public string? PhuongThucThanhToan { get; set; } // "ChuyenKhoan", "TienMat"
        public DateTime? NgayThanhToan { get; set; } // Thời gian thanh toán
        public decimal? TongTien { get; set; } // Tổng tiền


    }
}