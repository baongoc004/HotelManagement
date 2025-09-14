using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class KhachHang
    {
        // Mã định danh khách hàng (khóa chính), thường liên kết với PersonId
        public string KhachHangId { get; set; } = null!;
        // Thuộc tính điều hướng liên kết đến bảng Person (1-1)
        public virtual Person KhachHangNavigation { get; set; } = null!;
    }
}
