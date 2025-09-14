using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class NhanVien
    {
        public string NhanVienId { get; set; } = null!;
        public string MaVaiTro { get; set; } = null!;
        public DateTime? NgayDuocTuyen { get; set; }

        // Thuộc tính điều hướng đến vai trò của nhân viên (quan hệ với bảng VaiTro)
        public virtual VaiTro MaVaiTroNavigation { get; set; } = null!;

        // Thuộc tính điều hướng đến thông tin chung của nhân viên trong bảng Person (1-1)
        public virtual Person NhanVienNavigation { get; set; } = null!;
    }
}
