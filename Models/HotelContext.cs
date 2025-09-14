using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelManagement.Models
{
    public partial class HotelContext : DbContext // DbContext đại diện cho kết nối và ánh xạ đến CSDL
    {
        public HotelContext() // Constructor mặc định
        {
        }

        // Constructor có truyền tùy chọn cấu hình (dùng khi inject context)
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {

        }

        // DbSet ánh xạ từng bảng trong CSDL thành tập thực thể (table -> object)
        public virtual DbSet<DichVu> DichVus { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; } = null!;
        public virtual DbSet<LoaiTaiKhoan> LoaiTaiKhoans { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<OrderPhong> OrderPhongs { get; set; } = null!;
        public virtual DbSet<OrderPhongDichVu> OrderPhongDichVus { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<TrangThaiPhong> TrangThaiPhongs { get; set; } = null!;
        public virtual DbSet<VaiTro> VaiTros { get; set; } = null!;


        // Cấu hình chuỗi kết nối đến CSDL SQL Server nếu chưa cấu hình bên ngoài
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Kiểm tra nếu chưa được cấu hình thì sử dụng connection string mặc định
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hotel;Integrated Security=True");
            }
        }

        // Cấu hình ánh xạ bảng và mối quan hệ giữa các thực thể (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ====== DICH_VU ======
            modelBuilder.Entity<DichVu>(entity =>
            {
                // Thiết lập khóa chính cho entity DichVu
                entity.HasKey(e => e.MaDichVu)
                    .HasName("PK__Dich_Vu__C0E6DE8FC72B2A69");

                entity.ToTable("Dich_Vu"); // Mapping entity với bảng "Dich_Vu" trong database

                entity.Property(e => e.MaDichVu).HasMaxLength(255); // Cấu hình độ dài tối đa cho cột MaDichVu

                entity.Property(e => e.TenDichVu).HasMaxLength(255);
            });

            // ====== HOA_DON ======
            modelBuilder.Entity<HoaDon>(entity =>
            {
                // Thiết lập khóa chính cho entity HoaDon
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__Hoa_Don__835ED13B118615AA");

                entity.ToTable("Hoa_Don"); // Mapping entity với bảng "Hoa_Don" trong database

                entity.Property(e => e.MaHoaDon).HasMaxLength(255);

                entity.Property(e => e.MaOrderPhong).HasMaxLength(255);

                entity.Property(e => e.NgayIn).HasColumnType("datetime");

                // Thiết lập mối quan hệ foreign key với bảng OrderPhong
                entity.HasOne(d => d.MaOrderPhongNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaOrderPhong)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKHoa_Don624260");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("Khach_Hang"); // Mapping entity với bảng "Khach_Hang" trong database

                entity.Property(e => e.KhachHangId) // Cấu hình cột KhachHang_ID với độ dài và tên cột
                    .HasMaxLength(255)
                    .HasColumnName("KhachHang_ID");

                // Quan hệ 1-1 với bảng Person
                entity.HasOne(d => d.KhachHangNavigation)
                    .WithOne(p => p.KhachHang)
                    .HasForeignKey<KhachHang>(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKKhach_Hang279424");
            });
            
            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.HasKey(e => e.MaLoaiPhong) // Thiết lập khóa chính cho entity LoaiPhong
                    .HasName("PK__Loai_Pho__23021217A48C92C2");


                entity.ToTable("Loai_Phong"); // Mapping entity với bảng "Loai_Phong" trong database

                entity.Property(e => e.MaLoaiPhong).HasMaxLength(255);

                entity.Property(e => e.TenLoaiPhong).HasMaxLength(255);
            });

            modelBuilder.Entity<LoaiTaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaLoaiTaiKhoan)
                    .HasName("PK__Loai_Tai__5F6E141C07C4DC2B");

                entity.ToTable("Loai_Tai_Khoan");

                entity.Property(e => e.MaLoaiTaiKhoan).HasMaxLength(255);

                entity.Property(e => e.TenLoai).HasMaxLength(255);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("Nhan_Vien");

                // Cấu hình cột NhanVienID với độ dài và tên cột
                entity.Property(e => e.NhanVienId)
                    .HasMaxLength(255)
                    .HasColumnName("NhanVienID");

                entity.Property(e => e.MaVaiTro).HasMaxLength(255);

                // Cấu hình kiểu dữ liệu datetime cho cột NgayDuocTuyen
                entity.Property(e => e.NgayDuocTuyen).HasColumnType("datetime");

                // Thiết lập mối quan hệ foreign key với bảng VaiTro
                entity.HasOne(d => d.MaVaiTroNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaVaiTro)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKNhan_Vien799741");

                // Thiết lập mối quan hệ 1-1 với bảng Person
                entity.HasOne(d => d.NhanVienNavigation)
                    .WithOne(p => p.NhanVien)
                    .HasForeignKey<NhanVien>(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKNhan_Vien605300");
            });

            modelBuilder.Entity<OrderPhong>(entity =>
            {
                entity.HasKey(e => e.MaOrderPhong)
                    .HasName("PK__Order_Ph__829E7C7605A5F40A");

                entity.ToTable("Order_Phong");

                entity.Property(e => e.MaOrderPhong).HasMaxLength(255);

                entity.Property(e => e.MaPhong).HasMaxLength(255);

                // Cấu hình kiểu dữ liệu datetime cho cột NgayDen
                entity.Property(e => e.NgayDen).HasColumnType("datetime");

                entity.Property(e => e.NgayDi).HasColumnType("datetime");

                // Dòng comment - có thể là cấu hình trạng thái thanh toán (đã được comment out)
                //entity.Property(e => e.TrangThaiThanhToan).HasColumnType("int");

                // Cấu hình cột PersonID với độ dài và tên cột
                entity.Property(e => e.PersonId)
                    .HasMaxLength(255)
                    .HasColumnName("PersonID");

                // Thiết lập mối quan hệ foreign key với bảng Phong
                entity.HasOne(d => d.MaPhongNavigation)
                     .WithMany(p => p.OrderPhongs)
                     .HasForeignKey(d => d.MaPhong)
                     .OnDelete(DeleteBehavior.Cascade)
                     .HasConstraintName("FKOrder_Phon460975");

                // Thiết lập mối quan hệ foreign key với bảng Person
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.OrderPhongs)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder_Phon746646");
            });

            // Cấu hình entity OrderPhongDichVu (Đặt phòng - Dịch vụ - bảng trung gian)
            modelBuilder.Entity<OrderPhongDichVu>(entity =>
            {
                // Thiết lập khóa chính composite (kết hợp) từ 2 cột
                entity.HasKey(e => new { e.MaOrderPhong, e.MaDichVu })
                    .HasName("PK__Order_Ph__6E90119E9EC16A77");

                // Mapping entity với bảng "Order_Phong_Dich_Vu" trong database
                entity.ToTable("Order_Phong_Dich_Vu");

                entity.Property(e => e.MaOrderPhong).HasMaxLength(255);

                entity.Property(e => e.MaDichVu).HasMaxLength(255);

                // Thiết lập mối quan hệ foreign key với bảng DichVu
                entity.HasOne(d => d.MaDichVuNavigation)
                    .WithMany(p => p.OrderPhongDichVus)
                    .HasForeignKey(d => d.MaDichVu)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder_Phon597344");

                // Thiết lập mối quan hệ foreign key với bảng OrderPhong
                entity.HasOne(d => d.MaOrderPhongNavigation)
                    .WithMany(p => p.OrderPhongDichVus)
                    .HasForeignKey(d => d.MaOrderPhong)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder_Phon17642");
            });

            // Cấu hình entity Person
            modelBuilder.Entity<Person>(entity =>
            {
                // Mapping entity với bảng "Person" trong database
                entity.ToTable("Person");

                // Cấu hình cột PersonID với độ dài và tên cột
                entity.Property(e => e.PersonId)
                    .HasMaxLength(255)
                    .HasColumnName("PersonID");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(255)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.HoTen).HasMaxLength(255);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(255)
                    .HasColumnName("SDT");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__Phong__20BD5E5B177E3D28");

                entity.ToTable("Phong");

                entity.Property(e => e.MaPhong).HasMaxLength(255);

                entity.Property(e => e.MaLoaiPhong).HasMaxLength(255);

                entity.Property(e => e.MaTrangThai).HasMaxLength(255);

                entity.Property(e => e.MoTaPhong).HasMaxLength(255);

                entity.Property(e => e.TenPhong).HasMaxLength(255);

                // Thiết lập mối quan hệ foreign key với bảng LoaiPhong
                entity.HasOne(d => d.MaLoaiPhongNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.MaLoaiPhong)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKPhong134689");

                // Thiết lập mối quan hệ foreign key với bảng TrangThaiPhong
                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPhong128242");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan)
                    .HasName("PK__Tai_Khoa__AD7C6529EF10FB2D");

                entity.ToTable("Tai_Khoan");

                entity.Property(e => e.MaTaiKhoan).HasMaxLength(255);

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PersonId)
                    .HasMaxLength(255)
                    .HasColumnName("PersonID");

                entity.Property(e => e.UserName).HasMaxLength(255);

                // Thiết lập mối quan hệ foreign key với bảng LoaiTaiKhoan
                entity.HasOne(d => d.LoaiTaiKhoanNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.LoaiTaiKhoan)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKTai_Khoan92928");

                // Thiết lập mối quan hệ foreign key với bảng Person
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKTai_Khoan172310");
            });

            modelBuilder.Entity<TrangThaiPhong>(entity =>
            {
                entity.HasKey(e => e.MaTrangThai)
                    .HasName("PK__Trang_Th__AADE41383344BB34");

                // Mapping entity với bảng "Trang_Thai_Phong" trong database
                entity.ToTable("Trang_Thai_Phong");

                entity.Property(e => e.MaTrangThai).HasMaxLength(255);

                entity.Property(e => e.TenTrangThai).HasMaxLength(255);
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVaiTro)
                    .HasName("PK__Vai_Tro__C24C41CFA446BD32");

                // Mapping entity với bảng "Vai_Tro" trong database
                entity.ToTable("Vai_Tro");

                entity.Property(e => e.MaVaiTro).HasMaxLength(255);

                entity.Property(e => e.TenVaiTro).HasMaxLength(255);
            });
            // Gọi phương thức partial để cho phép cấu hình bổ sung trong file khác
            OnModelCreatingPartial(modelBuilder);
        }

        // Phương thức partial rỗng cho phép cấu hình bổ sung trong file khác
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
