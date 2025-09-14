using HotelManagement.Models;
namespace HotelManagement.DataAccess
{
    public interface IRepository
    {
        IEnumerable<Person> getPeople { get; } // Property lấy danh sách tất cả người dùng trong hệ thống
        bool CreateAccount(TaiKhoan a); // Phương thức tạo tài khoản mới, trả về true nếu thành công, false nếu thất bại
        TaiKhoan CheckAccount(TaiKhoan a); // Phương thức kiểm tra thông tin đăng nhập, trả về đối tượng TaiKhoan nếu hợp lệ
        string CreateMaTaiKhoan(); // Phương thức tạo mã tài khoản tự động
        IEnumerable<LoaiPhong> getLoaiPhong { get; } // Phương thức lấy danh sách tất cả loại phòng
        IEnumerable<Phong> getPhongByLoaiPhong(string id); // Phương thức lấy danh sách phòng theo loại phòng
        void removeLoaiPhong(string id); // Phương thức xóa loại phòng theo id
        void themLoaiPhong(LoaiPhong newloaiphong);
        void suaLoaiPhong(LoaiPhong phongcuasua);
        public IEnumerable<TrangThaiPhong> getTrangThaiPhong { get; }
        void themPhong(Phong newphong);
        void xoaPhong(string id);
        void suaPhong(Phong phongcansua);
        public IEnumerable<Phong> getPhongByMaTrangThai(string trangthai);
        public IEnumerable<DichVu> getDichvu { get; }
        string createOrderPhongId();
        void updateTrangThaiPhong(string maphong, string maTrangThai);
        void addKhachHang(KhachHang kh);
        void addOrderPhong(OrderPhong orderPhong);
        void addOrderPhongDichVu(List<OrderPhongDichVu> orderphongdichvu); // Phương thức thêm danh sách order phòng dịch vụ
        Phong getPhongByMaPhong(string id); // Phương thức lấy thông tin phòng theo mã phòng
        IEnumerable<OrderPhong> getOrderPhongByMaPhong(string id); // Phương thức lấy danh sách order phòng theo mã phòng
        string createHoaDonId(); // Phương thức tạo mã hóa đơn tự động
        void updateTrangThaiOrderPhong(string orderPhongId);
        //0 là chưa thanh toán
        //1 là đã thanh toán
        //2 là phòng đặt trước
        bool addHoaDon(HoaDon hoaDon);
        Person getPersonByUserName(string username); // Phương thức lấy thông tin người dùng theo username
        public IEnumerable<OrderPhong> getOrderPhongByPerson(string personid); // Phương thức lấy danh sách order phòng theo mã người dùng
        public int funcGetLastIndex(List<string> maid, int vt); // Phương thức lấy index cuối cùng từ danh sách mã id
        public void removeOrderPhong(string maorder);
        IEnumerable<HoaDon> GetHoaDon { get; }
        IEnumerable<HoaDon> getChiTietHoaDon(string mahoadon);
        IEnumerable<KhachHang> getKhachHang { get; }
        public void removeKhachHang(string makhachhang);
        IEnumerable<LoaiTaiKhoan> getLoaiTaiKhoan { get; }
        IEnumerable<TaiKhoan> getTaiKhoan { get; }
        IEnumerable<NhanVien> getTaiKhoanNhanVien { get; }
        IEnumerable<KhachHang> getTaiKhoanKhachHang { get; }
        IEnumerable<VaiTro> GetVaiTros { get; }
        bool checkTonTaiUserName(string username);
        bool checkTonTaiMaNhanVien(string manhanvien);

        bool addNhanVien(NhanVien nhanvien);
        bool addTaiKhoanNhanVien(TaiKhoan taiKhoan);
        void updateThongTinKhachHang(Person newperson); // Phương thức cập nhật thông tin khách hàng
        void updateTrangThaiPhongs(IEnumerable<Phong> phongs);
        IEnumerable<DichVu> getDichVus { get; }
        void updateDichVu(DichVu dichvu);
        bool xoaDichVu(string madichvu);
        bool themDichVu(DichVu dichvu);
        void updateThongTinNhanVien(Person nhanvien, NhanVien vaitro);
        void updateLoaiTaiKhoanOfPerson(string personID, string loaitaikhoan);
        bool removeNhanVien(string manhanvien);
        void updateLoaiTaiKhoan(LoaiTaiKhoan loaitaikhoancansua);
        bool updateTaiKhoan(string mataikhoan, string username, string password);

        // ===== CÁC PHƯƠNG THỨC MỚI CHO CHỨC NĂNG THANH TOÁN =====

        /// <summary>
        /// Lấy thông tin đơn đặt phòng theo mã order
        /// </summary>
        /// <param name="maorder">Mã order phòng</param>
        /// <returns>Thông tin OrderPhong hoặc null nếu không tìm thấy</returns>
        OrderPhong getOrderPhongByMa(string maorder);

        /// <summary>
        /// Cập nhật trạng thái thanh toán của đơn đặt phòng
        /// </summary>
        /// <param name="maorder">Mã order phòng</param>
        /// <param name="phuongThucThanhToan">Phương thức thanh toán: "ChuyenKhoan" hoặc "TienMat"</param>
        /// <param name="trangThaiThanhToan">Trạng thái thanh toán: "DaThanhToan" hoặc "ChuaThanhToan"</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại</returns>
        bool updatePaymentStatus(string maorder, string phuongThucThanhToan, string trangThaiThanhToan);
    }
}