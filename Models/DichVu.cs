using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public partial class DichVu // Lớp đại diện cho bảng Dịch Vụ trong CSDL
    {
        // Constructor khởi tạo danh sách các đơn đặt dịch vụ liên quan
        public DichVu()
        {
            // Khởi tạo collection OrderPhongDichVus như một HashSet rỗng
            // HashSet đảm bảo không có phần tử trùng lặp và tối ưu hiệu suất tìm kiếm
            OrderPhongDichVus = new HashSet<OrderPhongDichVu>();
        }

        public string MaDichVu { get; set; } = null!;  // Mã dịch vụ (khóa chính) - không được null
        public string? TenDichVu { get; set; } // Tên dịch vụ - có thể null
        public float GiaDichVu { get; set; } // Giá dịch vụ - kiểu float

        // Danh sách các đơn đặt dịch vụ (liên kết với bảng OrderPhongDichVu - quan hệ 1-nhiều)
        public virtual ICollection<OrderPhongDichVu> OrderPhongDichVus { get; set; }
    }
}
