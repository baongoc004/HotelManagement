using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class LoaiTaiKhoan
    {
        public LoaiTaiKhoan()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public string MaLoaiTaiKhoan { get; set; } = null!;
        public string? TenLoai { get; set; }

        // Danh sách các tài khoản thuộc loại này (1 loại tài khoản có nhiều tài khoản cụ thể)
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
