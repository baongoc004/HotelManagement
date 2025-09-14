using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class Phong
    {
        public Phong()
        {
            OrderPhongs = new HashSet<OrderPhong>();
        }

        public string MaPhong { get; set; } = null!;
        public string? TenPhong { get; set; }
        public string? MoTaPhong { get; set; }
        public string MaTrangThai { get; set; } = null!;
        public string MaLoaiPhong { get; set; } = null!;

        // Điều hướng đến loại phòng tương ứng (1 phòng thuộc 1 loại)
        public virtual LoaiPhong MaLoaiPhongNavigation { get; set; } = null!;

        // Điều hướng đến trạng thái phòng hiện tại
        public virtual TrangThaiPhong MaTrangThaiNavigation { get; set; } = null!;

        // Danh sách các đơn đặt phòng liên quan đến phòng này (1 phòng có thể được đặt nhiều lần)
        public virtual ICollection<OrderPhong> OrderPhongs { get; set; }
    }
}