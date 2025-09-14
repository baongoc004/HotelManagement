using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Transactions;

namespace HotelManagement.DataAccess
{
    public class Repository : IRepository
    {
        private HotelContext context;
        private IHttpContextAccessor accessor;
        public Repository(HotelContext context, IHttpContextAccessor accessor)
        {

            this.context = context;
            this.accessor = accessor;
        }

        public IEnumerable<Person> getPeople => this.context.People;


        public TaiKhoan CheckAccount(TaiKhoan a)
        {
            var matchingAccount = this.context.TaiKhoans.FirstOrDefault(account => account.UserName == a.UserName && account.Password == a.Password);
            if (matchingAccount == null) return null;
            return matchingAccount; // Trả về tài khoản tìm được
        }

        public bool CreateAccount(TaiKhoan a) // Phương thức tạo tài khoản mới
        {
            var checkTrungUserName = context.TaiKhoans.Where(account => account.UserName == a.UserName).Any();
            if (checkTrungUserName)//nếu có return false
            {
                return false;
            }
            else
            {
                context.TaiKhoans.Add(a);
                var check = context.SaveChanges();
                if (check > 0) // Nếu được lưu thành công
                {
                    context.KhachHangs.Add(new KhachHang { KhachHangId = a.PersonId });
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
        }

        public string CreateMaTaiKhoan()
        {
            if (context.TaiKhoans.Any() == false) // Nếu chưa có tài khoản nào
            {
                return "TK1"; // Trả về mã đầu tiên
            }
            // Lấy danh sách tất cả mã tài khoản hiện có
            List<string> maTaiKhoan = context.TaiKhoans.Select(tk => tk.MaTaiKhoan).ToList();
            int lasId = funcGetLastIndex(maTaiKhoan, 2) + 1; // Tìm số thứ tự lớn nhất và cộng thêm 1
            return "TK" + lasId; // Trả về mã tài khoản mới
        }

        public IEnumerable<LoaiPhong> getLoaiPhong => context.LoaiPhongs;


        // Phương thức lấy danh sách phòng theo loại phòng
        public IEnumerable<Phong> getPhongByLoaiPhong(string id)
        {
            if (id == null)
            {
                var s1 = context.Phongs; // Lấy tất cả phòng
                var s2 = s1 // Include( truy vấn DL) các order phòng chưa thanh toán (status = 0)
                    .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                    .ThenInclude(od => od.Person) // Include thông tin người đặt
                    .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                    .ThenInclude(od => od.OrderPhongDichVus) // Include dịch vụ đặt kèm
                    .ThenInclude(odpdv => odpdv.MaDichVuNavigation); // Include chi tiết dịch vụ
                return s2; 
            }

            // Lọc phòng theo loại phòng
            var s3 = context.Phongs.Where(p => p.MaLoaiPhong == id);
            var s4 = s3
                .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                .ThenInclude(od => od.Person)
                .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                .ThenInclude(od => od.OrderPhongDichVus)
                .ThenInclude(odpdv => odpdv.MaDichVuNavigation);
            return s4;
        }

        public void removeLoaiPhong(string id)
        {
            //khi xóa loaiphong thì tất cả các phòng thuộc loại phòng đó đều bị xóa
            //do trong file context Phong có .Ondelete là cascade

            // Tìm loại phòng cần xóa
            var loaiphong = context.LoaiPhongs.Where(lp => lp.MaLoaiPhong == id).FirstOrDefault();
            context.Remove(loaiphong);
            context.SaveChanges();
        }

        public void themLoaiPhong(LoaiPhong newloaiphong)
        {
            context.LoaiPhongs.Add(newloaiphong);
            context.SaveChanges();
        }

        public void suaLoaiPhong(LoaiPhong phongcuasua)
        {
            context.Update(phongcuasua);
            context.SaveChanges();
        }

        public IEnumerable<TrangThaiPhong> getTrangThaiPhong => context.TrangThaiPhongs;


        public void themPhong(Phong newphong)
        {
            context.Phongs.Add(newphong);
            context.SaveChanges();
        }

        public void xoaPhong(string id)
        {
            // Tìm phòng theo mã và xóa
            context.Phongs.Remove(context.Phongs.Where(p => p.MaPhong == id).FirstOrDefault());
            context.SaveChanges();
        }

        public void suaPhong(Phong phongcansua)
        {
            context.Phongs.Update(phongcansua);
            context.SaveChanges();
        }

        // Phương thức lấy danh sách phòng theo trạng thái
        public IEnumerable<Phong> getPhongByMaTrangThai(string trangthai)
        {
            // Lọc phòng theo trạng thái và include thông tin order phòng chưa thanh toán
            return context.Phongs.Where(p => p.MaTrangThai == trangthai)
                .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                .ThenInclude(od => od.Person)
                .Include(p => p.OrderPhongs.Where(od => od.TrangThaiThanhToan == 0))
                .ThenInclude(od => od.OrderPhongDichVus)
                .ThenInclude(odpdv => odpdv.MaDichVuNavigation);
        }

        public IEnumerable<DichVu> getDichvu => context.DichVus;

        // Phương thức tạo mã order phòng tự động
        public string createOrderPhongId()
        {
            bool check = context.OrderPhongs.Any(); // Kiểm tra có order phòng nào chưa
            if (check == false) // Nếu chưa có order nào
            {
                return "MOP1"; // Trả về mã đầu tiên
            }

            // Lấy danh sách tất cả mã order phòng
            var maOrderPhongs = context.OrderPhongs.Select(o => o.MaOrderPhong).ToList();
            // Tìm số thứ tự lớn nhất và cộng thêm 1
            int lastIndex = funcGetLastIndex(maOrderPhongs, 3) + 1;
            return "MOP" + lastIndex; // Trả về mã order phòng mới

        }

        public void updateTrangThaiPhong(string maphong, string maTrangThai)
        {
            // Tìm phòng cần cập nhật trạng thái
            Phong phongCanSuaTrangThai = context.Phongs.Where(p => p.MaPhong == maphong).FirstOrDefault();
            phongCanSuaTrangThai.MaTrangThai = maTrangThai; // Cập nhật trạng thái mới
            context.Phongs.Update(phongCanSuaTrangThai);
            context.SaveChanges();
        }

        public void addKhachHang(KhachHang kh)
        {
            // Kiểm tra khách hàng đã tồn tại chưa
            if (context.KhachHangs.Find(kh.KhachHangId) == null)
            {
                context.KhachHangs.Add(kh);
                context.SaveChanges();
            }
        }


        public void addOrderPhong(OrderPhong orderPhong)
        {
            // Kiểm tra người đặt phòng đã tồn tại chưa
            if (context.People.Where(p => p.PersonId == orderPhong.PersonId).Any())
            {
                context.People.Update(orderPhong.Person); // Cập nhật thông tin người đặt
                context.SaveChanges();
            }
            //khi add orderPhong thi thông tin người order cũng được lưu tại vì trong orderPhong có Person 
            context.OrderPhongs.Add(orderPhong);
            context.SaveChanges();

            KhachHang kh = new KhachHang
            {
                KhachHangId = orderPhong.PersonId,
            };
            //nếu người đặt phòng không phải là user
            if (accessor.HttpContext.Session.GetString("UserName") == null)
            {
                addKhachHang(kh); // Thêm vào danh sách khách hàng
            }

        }

        // Phương thức thêm danh sách dịch vụ order phòng
        public void addOrderPhongDichVu(List<OrderPhongDichVu> orderphongdichvu)
        {
            context.OrderPhongDichVus.AddRange(orderphongdichvu); // Thêm tất cả dịch vụ order
            context.SaveChanges();
        }

        // Phương thức lấy thông tin phòng theo mã phòng
        public Phong getPhongByMaPhong(string id)
        {
            return context.Phongs.Where(p => p.MaPhong == id).FirstOrDefault(); // Tìm và trả về phòng
        }


        public IEnumerable<OrderPhong> getOrderPhongByMaPhong(string id)
        {
            if (id == null) // Nếu không chỉ định mã phòng
            {
                // Trả về tất cả order phòng với thông tin đầy đủ
                return context.OrderPhongs.
                    Include(o => o.Person). // Include thông tin người đặt
                    Include(o => o.MaPhongNavigation).ThenInclude(p => p.MaLoaiPhongNavigation). // Include thông tin phòng và loại phòng
                    Include(o => o.OrderPhongDichVus). // Include dịch vụ đặt kèm
                    ThenInclude(od => od.MaDichVuNavigation); // Include chi tiết dịch vụ
            }

            else
            {
                // Lọc theo mã phòng và include thông tin tương tự
                return context.OrderPhongs.
                    Where(o => o.MaPhong == id).
                    Include(o => o.Person).
                    Include(o => o.MaPhongNavigation).ThenInclude(p => p.MaLoaiPhongNavigation).
                    Include(o => o.OrderPhongDichVus).
                    ThenInclude(od => od.MaDichVuNavigation);
            }

        }

        public string createHoaDonId() // Phương thức tạo mã hóa đơn tự động
        {
            if (context.HoaDons.Any() == false) return "HD1"; // Nếu chưa có hóa đơn nào
            // Lấy danh sách tất cả mã hóa đơn
            List<string> maHoaDon = context.HoaDons.Select(hd => hd.MaHoaDon).ToList();
            // Tìm số thứ tự lớn nhất và cộng thêm 1
            int lastId = funcGetLastIndex(maHoaDon, 2) + 1;
            return "HD" + lastId; // Trả về mã hóa đơn mới
        }

        public bool addHoaDon(HoaDon hoaDon)
        {
            // Kiểm tra đã có hóa đơn cho order phòng này chưa
            var check = context.HoaDons.Where(hd => hd.MaOrderPhong == hoaDon.MaOrderPhong).Any();
            if (check) return false; // Đã có hóa đơn, trả về false
            else
            {
                context.HoaDons.Add(hoaDon);
                var checkSave = context.SaveChanges();
                if (checkSave != 0) return true; // Lưu thành công
                else return false; // Lưu thất bại
            }
        }

        public void updateTrangThaiOrderPhong(string orderPhongId)
        {
            // Tìm order phòng cần cập nhật
            OrderPhong od = context.OrderPhongs.FirstOrDefault(od => od.MaOrderPhong == orderPhongId);
            od.TrangThaiThanhToan = 1; // cập nhật lại là đã thanh toán
            context.OrderPhongs.Update(od); // Cập nhật vào context
            context.SaveChanges();

        }

        // Phương thức lấy thông tin người dùng theo username
        public Person getPersonByUserName(string username)
        {
            //C1 :
            //đầu tiền cần xác định userName này của tài khoản nào
            TaiKhoan taiKhoan = context.TaiKhoans.FirstOrDefault(tk => tk.UserName == username);

            //tiếp theo từ tài khoản lấy ra PersonId;
            string personId = taiKhoan.PersonId;

            //cuối cùng lấy Person dự trên PersonId
            return context.People.FirstOrDefault(p => p.PersonId == personId);

            ////C2 : từ toài khoản lấy luôn person bằng EF
            //var tk = context.TaiKhoans.Where(tk => tk.UserName == username).
            //    Include( tk => tk.Person).FirstOrDefault();
            //return tk.Person;
        }

        // Phương thức lấy danh sách order phòng theo person ID
        public IEnumerable<OrderPhong> getOrderPhongByPerson(string personid)
        {
            // Lấy order phòng chưa thanh toán của person
            var result = context.OrderPhongs.
                Include(od => od.MaPhongNavigation). // Include thông tin phòng
                Where(od => od.PersonId == personid && od.TrangThaiThanhToan == 0);
            return result;
        }

        // Phương thức tìm index lớn nhất từ danh sách mã ID
        public int funcGetLastIndex(List<string> maid, int vt)
        {
            // Parse phần số từ mã ID (bỏ prefix) và lọc các số hợp lệ
            List<int> STT = maid.Select(ma => int.TryParse(ma.Substring(vt), out int number) ? number : -1).Where(number => number != -1).ToList();
            STT.Sort(); // Sắp xếp tăng dần
            return STT[STT.Count - 1]; // Trả về số lớn nhất
        }

        public void removeOrderPhong(string maorder)
        {
            // Tìm order phòng cần xóa
            OrderPhong order = context.OrderPhongs.FirstOrDefault(od => od.MaOrderPhong == maorder);
            context.OrderPhongs.Remove(order);
            context.SaveChanges();
        }

        // Property trả về danh sách tất cả hóa đơn với thông tin order phòng
        public IEnumerable<HoaDon> GetHoaDon => context.HoaDons.Include(hd => hd.MaOrderPhongNavigation);

        public IEnumerable<HoaDon> getChiTietHoaDon(string mahoadon)
        {
            // Lọc hóa đơn theo mã
            var hoadon = context.HoaDons.Where(hd => hd.MaHoaDon == mahoadon);
            // Include tất cả thông tin liên quan: order phòng, phòng, loại phòng, dịch vụ, người đặt
            return hoadon.Include(hd => hd.MaOrderPhongNavigation)
                         .ThenInclude(od => od.MaPhongNavigation)
                         .ThenInclude(od => od.MaLoaiPhongNavigation)
                         .Include(hd => hd.MaOrderPhongNavigation)
                         .ThenInclude(od => od.OrderPhongDichVus)
                         .ThenInclude(odpdv => odpdv.MaDichVuNavigation)
                         .Include(hd => hd.MaOrderPhongNavigation)
                         .ThenInclude(od => od.Person);
        }

        // Trả về danh sách khách hàng với thông tin người và order phòng
        public IEnumerable<KhachHang> getKhachHang => context.KhachHangs.Include(kh => kh.KhachHangNavigation)
                                                                        .ThenInclude(p => p.OrderPhongs);

        public void removeKhachHang(string makhachhang)
        {
            Person person = context.People.Where(p => p.PersonId == makhachhang).FirstOrDefault();
            context.People.Remove(person);
            context.SaveChanges();
        }


        public IEnumerable<LoaiTaiKhoan> getLoaiTaiKhoan => context.LoaiTaiKhoans;

        public IEnumerable<TaiKhoan> getTaiKhoan => context.TaiKhoans;

        // Property trả về danh sách nhân viên với thông tin vai trò và tài khoản
        public IEnumerable<NhanVien> getTaiKhoanNhanVien => context.NhanViens
            .Include(nv => nv.MaVaiTroNavigation) // Include thông tin vai trò
            .Include(nv => nv.NhanVienNavigation) // Include thông tin person
            .ThenInclude(p => p.TaiKhoans); // Include tài khoản của person

        // Property trả về danh sách khách hàng với thông tin person và tài khoản
        public IEnumerable<KhachHang> getTaiKhoanKhachHang => context.KhachHangs
            .Include(kh => kh.KhachHangNavigation) // Include thông tin person
            .ThenInclude(p => p.TaiKhoans); // Include tài khoản của person

        public IEnumerable<VaiTro> GetVaiTros => context.VaiTros;

        // Phương thức kiểm tra tồn tại username
        public bool checkTonTaiUserName(string username)
        {
            bool check = context.TaiKhoans.Where(tk => tk.UserName == username).Any();
            if (check) return true; // Có tồn tại
            return false; // Không tồn tại
        }

        public bool checkTonTaiMaNhanVien(string manhanvien)
        {
            bool check = context.NhanViens.Where(nv => nv.NhanVienId == manhanvien).Any();
            if (check) return true;
            return false;

        }

        public bool addNhanVien(NhanVien nhanvien)
        {
            //khi add nhan vien thi person cũng add theo 
            context.NhanViens.Add(nhanvien); // Thêm nhân viên
            int check = context.SaveChanges();
            if (check > 0) return true;
            return false;
        }

        public bool addTaiKhoanNhanVien(TaiKhoan taiKhoan)
        {
            context.TaiKhoans.Add(taiKhoan);
            int check = context.SaveChanges();
            if (check > 0) return true;
            return false;
        }

        public void updateTrangThaiPhongs(IEnumerable<Phong> phongs)
        {
            context.Phongs.UpdateRange(phongs); // Cập nhật nhiều phòng
            context.SaveChanges();
        }


        public void updateThongTinKhachHang(Person newperson)
        {


            context.People.Update(newperson);
            context.SaveChanges();
        }

        public IEnumerable<DichVu> getDichVus => context.DichVus;


        public void updateDichVu(DichVu dichvu)
        {
            context.DichVus.Update(dichvu);
            context.SaveChanges();
        }

        public bool xoaDichVu(string madichvu)
        {
            // Tìm dịch vụ cần xóa
            DichVu dichvu = context.DichVus.FirstOrDefault(dv => dv.MaDichVu == madichvu);
            if (dichvu == null) return false; // Không tìm thấy
            else
            {
                context.DichVus.Remove(dichvu);
                context.SaveChanges();
                return true;
            }
        }

        public bool themDichVu(DichVu dichvu)
        {
            // Kiểm tra dịch vụ đã tồn tại chưa
            var dv = context.DichVus.FirstOrDefault(dv => dv.MaDichVu == dichvu.MaDichVu);
            if (dv != null) // Đã tồn tại
            {
                return false;
            }
            else
            {
                context.DichVus.Add(dichvu);
                context.SaveChanges();
                return true;
            }
        }


        public void updateThongTinNhanVien(Person nhanvien, NhanVien vaitro)
        {
            context.People.Update(nhanvien); // Cập nhật thông tin person
            context.NhanViens.Update(vaitro); // Cập nhật thông tin nhân viên
            context.SaveChanges();
        }

        public void updateLoaiTaiKhoanOfPerson(string personID, string loaitaikhoan)
        {
            // Lấy tất cả tài khoản của person và cập nhật loại tài khoản
            var taikhoan = context.TaiKhoans.Where(tk => tk.PersonId == personID).ToList()
                .Select(tk =>
                {
                    tk.LoaiTaiKhoan = loaitaikhoan; // Cập nhật loại tài khoản
                    return tk;
                });


            context.TaiKhoans.UpdateRange(taikhoan); // Cập nhật nhiều tài khoản
            context.SaveChanges();

        }
        public bool removeNhanVien(string manhanvien)
        {
            Person nhanvien = context.People.FirstOrDefault(p => p.PersonId == manhanvien);
            if (nhanvien == null)
            { // Kiểm tra xem nhân viên có tồn tại không
                return false; // Nếu không tìm thấy
            }
            else
            {
                context.People.Remove(nhanvien);
                context.SaveChanges() ;
                return true;
            }
        }

        public void updateLoaiTaiKhoan(LoaiTaiKhoan loaitaikhoancansua)
        {
            context.LoaiTaiKhoans.Update(loaitaikhoancansua);
            context.SaveChanges() ; 
        }

        public bool updateTaiKhoan(string mataikhoan,string username, string password)
        {
            // Tìm kiếm tài khoản trong database dựa trên MaTaiKhoan
            TaiKhoan check = context.TaiKhoans.FirstOrDefault(tk => tk.MaTaiKhoan == mataikhoan);
            if (check == null)
            {
                return false; // Nếu không tìm thấy
            }
            else
            {
                check.UserName = username; // Cập nhật tên người dùng mới
                check.Password = password;
                context.TaiKhoans.Update(check);
                context.SaveChanges() ;
                return true;
            }
        }

        // ===== THÊM 2 METHOD NÀY VÀO CUỐI CLASS REPOSITORY CỦA BẠN =====

        /// <summary>
        /// Lấy thông tin đơn đặt phòng theo mã order
        /// </summary>
        /// <param name="maorder">Mã order phòng</param>
        /// <returns>Thông tin OrderPhong hoặc null nếu không tìm thấy</returns>
        public OrderPhong getOrderPhongByMa(string maorder)
        {
            try
            {
                // Lấy OrderPhong theo mã với đầy đủ thông tin liên quan
                return context.OrderPhongs
                    .Include(o => o.MaPhongNavigation) // Include thông tin phòng
                    .ThenInclude(p => p.MaLoaiPhongNavigation) // Include thông tin loại phòng
                    .Include(o => o.Person) // Include thông tin người đặt
                    .Include(o => o.OrderPhongDichVus) // Include dịch vụ đặt kèm
                    .ThenInclude(opdv => opdv.MaDichVuNavigation) // Include chi tiết dịch vụ
                    .FirstOrDefault(o => o.MaOrderPhong == maorder);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Lỗi khi lấy OrderPhong: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái thanh toán của đơn đặt phòng
        /// </summary>
        /// <param name="maorder">Mã order phòng</param>
        /// <param name="phuongThucThanhToan">Phương thức thanh toán: "ChuyenKhoan" hoặc "TienMat"</param>
        /// <param name="trangThaiThanhToan">Trạng thái thanh toán: "DaThanhToan" hoặc "ChuaThanhToan"</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại</returns>
        public bool updatePaymentStatus(string maorder, string phuongThucThanhToan, string trangThaiThanhToan)
        {
            try
            {
                // Tìm OrderPhong theo mã
                var orderPhong = context.OrderPhongs.FirstOrDefault(o => o.MaOrderPhong == maorder);

                if (orderPhong == null)
                {
                    return false; // Không tìm thấy order
                }

                // Cập nhật trạng thái thanh toán thành 1 (đã thanh toán)
                // Vì model hiện tại chỉ có TrangThaiThanhToan là int
                orderPhong.TrangThaiThanhToan = 1;

                // NOTE: Nếu bạn muốn lưu thêm thông tin phương thức thanh toán và ngày thanh toán,
                // hãy thêm các property sau vào model OrderPhong:
                // public string? PhuongThucThanhToan { get; set; }
                // public DateTime? NgayThanhToan { get; set; }
                // 
                // Sau đó uncomment các dòng dưới:
                // orderPhong.PhuongThucThanhToan = phuongThucThanhToan;
                // orderPhong.NgayThanhToan = DateTime.Now;

                // Lưu thay đổi vào database
                context.OrderPhongs.Update(orderPhong);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Lỗi khi cập nhật trạng thái thanh toán: {ex.Message}");
                return false;
            }
        }


    }
}
