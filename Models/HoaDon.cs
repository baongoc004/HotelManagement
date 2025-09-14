using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class HoaDon
    {
        public string MaHoaDon { get; set; } = null!; // Mã hóa đơn (khóa chính) - không được null
        public DateTime? NgayIn { get; set; }  // Ngày in hóa đơn - có thể null (nullable)
        public float TongTien { get; set; }  // Tổng tiền của hóa đơn - kiểu float
        public string MaOrderPhong { get; set; } = null!;  // Mã đơn đặt phòng liên kết (khóa ngoại) - không được null

        // Thuộc tính điều hướng đến OrderPhong (thiết lập quan hệ với bảng OrderPhong)
        public virtual OrderPhong MaOrderPhongNavigation { get; set; } = null!;
    }
}
