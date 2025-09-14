using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class LoaiPhong
    {
        public LoaiPhong()  // Constructor khởi tạo danh sách phòng (tránh null khi truy cập)
        {
            Phongs = new HashSet<Phong>();
        }

        public string MaLoaiPhong { get; set; } = null!; // Mã loại phòng (khóa chính) - không được null
        public string? TenLoaiPhong { get; set; }
        public float GiaPhong { get; set; }

        // Danh sách các phòng thuộc loại phòng này (quan hệ 1 - nhiều)
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
