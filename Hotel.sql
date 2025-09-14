CREATE DATABASE [Hotel]
USE [Hotel]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dich_Vu]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dich_Vu](
	[MaDichVu] [nvarchar](255) NOT NULL,
	[TenDichVu] [nvarchar](255) NULL,
	[GiaDichVu] [real] NOT NULL,
 CONSTRAINT [PK__Dich_Vu__C0E6DE8FC72B2A69] PRIMARY KEY CLUSTERED 
(
	[MaDichVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hoa_Don]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hoa_Don](
	[MaHoaDon] [nvarchar](255) NOT NULL,
	[NgayIn] [datetime] NULL,
	[TongTien] [real] NOT NULL,
	[MaOrderPhong] [nvarchar](255) NULL,
 CONSTRAINT [PK__Hoa_Don__835ED13B118615AA] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khach_Hang]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khach_Hang](
	[KhachHang_ID] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Khach_Hang] PRIMARY KEY CLUSTERED 
(
	[KhachHang_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai_Phong]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai_Phong](
	[MaLoaiPhong] [nvarchar](255) NOT NULL,
	[TenLoaiPhong] [nvarchar](255) NULL,
	[GiaPhong] [real] NOT NULL,
 CONSTRAINT [PK__Loai_Pho__23021217A48C92C2] PRIMARY KEY CLUSTERED 
(
	[MaLoaiPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai_Tai_Khoan]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai_Tai_Khoan](
	[MaLoaiTaiKhoan] [nvarchar](255) NOT NULL,
	[TenLoai] [nvarchar](255) NULL,
 CONSTRAINT [PK__Loai_Tai__5F6E141C07C4DC2B] PRIMARY KEY CLUSTERED 
(
	[MaLoaiTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhan_Vien]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhan_Vien](
	[NhanVienID] [nvarchar](255) NOT NULL,
	[MaVaiTro] [nvarchar](255) NULL,
	[NgayDuocTuyen] [datetime] NULL,
 CONSTRAINT [PK_Nhan_Vien] PRIMARY KEY CLUSTERED 
(
	[NhanVienID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Phong]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Phong](
	[MaOrderPhong] [nvarchar](255) NOT NULL,
	[NgayDen] [datetime] NULL,
	[NgayDi] [datetime] NULL,
	[PersonID] [nvarchar](255) NULL,
	[MaPhong] [nvarchar](255) NULL,
	[TrangThaiThanhToan] [int] NOT NULL,
 CONSTRAINT [PK__Order_Ph__829E7C7605A5F40A] PRIMARY KEY CLUSTERED 
(
	[MaOrderPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Phong_Dich_Vu]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Phong_Dich_Vu](
	[MaOrderPhong] [nvarchar](255) NOT NULL,
	[MaDichVu] [nvarchar](255) NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [real] NULL,
 CONSTRAINT [PK__Order_Ph__6E90119E9EC16A77] PRIMARY KEY CLUSTERED 
(
	[MaOrderPhong] ASC,
	[MaDichVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [nvarchar](255) NOT NULL,
	[HoTen] [nvarchar](255) NULL,
	[Tuoi] [int] NOT NULL,
	[GioiTinh] [int] NOT NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](255) NULL,
	[CCCD] [nvarchar](255) NULL,
	[SDT] [nvarchar](255) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[MaPhong] [nvarchar](255) NOT NULL,
	[TenPhong] [nvarchar](255) NULL,
	[MoTaPhong] [nvarchar](255) NULL,
	[MaTrangThai] [nvarchar](255) NULL,
	[MaLoaiPhong] [nvarchar](255) NULL,
 CONSTRAINT [PK__Phong__20BD5E5B177E3D28] PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tai_Khoan]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tai_Khoan](
	[MaTaiKhoan] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[LoaiTaiKhoan] [nvarchar](255) NULL,
	[PersonID] [nvarchar](255) NULL,
 CONSTRAINT [PK__Tai_Khoa__AD7C6529EF10FB2D] PRIMARY KEY CLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trang_Thai_Phong]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trang_Thai_Phong](
	[MaTrangThai] [nvarchar](255) NOT NULL,
	[TenTrangThai] [nvarchar](255) NULL,
 CONSTRAINT [PK__Trang_Th__AADE41383344BB34] PRIMARY KEY CLUSTERED 
(
	[MaTrangThai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vai_Tro]    Script Date: 10/08/2025 12:30:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vai_Tro](
	[MaVaiTro] [nvarchar](255) NOT NULL,
	[TenVaiTro] [nvarchar](255) NULL,
 CONSTRAINT [PK__Vai_Tro__C24C41CFA446BD32] PRIMARY KEY CLUSTERED 
(
	[MaVaiTro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116114016_second', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116155141_third', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231117091118_fourth', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231117122303_fifth', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231118111549_sixth', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231119053344_seventh', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231119064336_eighth', N'6.0.24')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231119094846_nineth', N'6.0.24')
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV1', N'Pepsi', 10000)
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV2', N'Coca', 10000)
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV3', N'Sting', 10000)
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV4', N'Bia', 25000)
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV5', N'Mì xào', 30000)
GO
INSERT [dbo].[Dich_Vu] ([MaDichVu], [TenDichVu], [GiaDichVu]) VALUES (N'DV6', N'Nước Lọc', 20000)
GO
INSERT [dbo].[Hoa_Don] ([MaHoaDon], [NgayIn], [TongTien], [MaOrderPhong]) VALUES (N'HD2', CAST(N'2025-06-13T13:14:49.497' AS DateTime), 1230000, N'MOP5')
GO
INSERT [dbo].[Khach_Hang] ([KhachHang_ID]) VALUES (N'3455464')
GO
INSERT [dbo].[Khach_Hang] ([KhachHang_ID]) VALUES (N'65343245')
GO
INSERT [dbo].[Loai_Phong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (N'MLP1', N'Phòng đơn', 150000)
GO
INSERT [dbo].[Loai_Phong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (N'MLP2', N'Phòng đôi', 280000)
GO
INSERT [dbo].[Loai_Phong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (N'MLP3', N'Phòng gia đình', 480000)
GO
INSERT [dbo].[Loai_Phong] ([MaLoaiPhong], [TenLoaiPhong], [GiaPhong]) VALUES (N'MLP4', N'Phòng đôi VIP', 1000000)
GO
INSERT [dbo].[Loai_Tai_Khoan] ([MaLoaiTaiKhoan], [TenLoai]) VALUES (N'LTK1', N'admin')
GO
INSERT [dbo].[Loai_Tai_Khoan] ([MaLoaiTaiKhoan], [TenLoai]) VALUES (N'LTK2', N'nhân viên')
GO
INSERT [dbo].[Loai_Tai_Khoan] ([MaLoaiTaiKhoan], [TenLoai]) VALUES (N'LTK3', N'khách hàng')
GO
INSERT [dbo].[Nhan_Vien] ([NhanVienID], [MaVaiTro], [NgayDuocTuyen]) VALUES (N'NV1', N'MVT1', NULL)
GO
INSERT [dbo].[Nhan_Vien] ([NhanVienID], [MaVaiTro], [NgayDuocTuyen]) VALUES (N'NV3', N'MVT2', NULL)
GO
INSERT [dbo].[Order_Phong] ([MaOrderPhong], [NgayDen], [NgayDi], [PersonID], [MaPhong], [TrangThaiThanhToan]) VALUES (N'MOP5', CAST(N'2025-06-25T00:00:00.000' AS DateTime), CAST(N'2025-06-26T00:00:00.000' AS DateTime), N'65343245', N'P10', 1)
GO
INSERT [dbo].[Order_Phong] ([MaOrderPhong], [NgayDen], [NgayDi], [PersonID], [MaPhong], [TrangThaiThanhToan]) VALUES (N'MOP6', CAST(N'2025-06-18T00:00:00.000' AS DateTime), CAST(N'2025-06-19T00:00:00.000' AS DateTime), N'65343245', N'P4', 0)
GO
INSERT [dbo].[Order_Phong_Dich_Vu] ([MaOrderPhong], [MaDichVu], [SoLuong], [DonGia]) VALUES (N'MOP5', N'DV1', 1, 10000)
GO
INSERT [dbo].[Order_Phong_Dich_Vu] ([MaOrderPhong], [MaDichVu], [SoLuong], [DonGia]) VALUES (N'MOP5', N'DV2', 2, 10000)
GO
INSERT [dbo].[Order_Phong_Dich_Vu] ([MaOrderPhong], [MaDichVu], [SoLuong], [DonGia]) VALUES (N'MOP5', N'DV3', 2, 10000)
GO
INSERT [dbo].[Order_Phong_Dich_Vu] ([MaOrderPhong], [MaDichVu], [SoLuong], [DonGia]) VALUES (N'MOP5', N'DV4', 1, 150000)
GO
INSERT [dbo].[Order_Phong_Dich_Vu] ([MaOrderPhong], [MaDichVu], [SoLuong], [DonGia]) VALUES (N'MOP5', N'DV5', 1, 30000)
GO
INSERT [dbo].[Person] ([PersonID], [HoTen], [Tuoi], [GioiTinh], [NgaySinh], [DiaChi], [CCCD], [SDT]) VALUES (N'3455464', N'khach test', 22, 1, NULL, NULL, NULL, N'432352')
GO
INSERT [dbo].[Person] ([PersonID], [HoTen], [Tuoi], [GioiTinh], [NgaySinh], [DiaChi], [CCCD], [SDT]) VALUES (N'65343245', N'Bảo Ngoc', 22, 1, NULL, NULL, NULL, N'876543')
GO
INSERT [dbo].[Person] ([PersonID], [HoTen], [Tuoi], [GioiTinh], [NgaySinh], [DiaChi], [CCCD], [SDT]) VALUES (N'NV1', N'Quản Lý', 21, 0, NULL, NULL, NULL, N'0913412342')
GO
INSERT [dbo].[Person] ([PersonID], [HoTen], [Tuoi], [GioiTinh], [NgaySinh], [DiaChi], [CCCD], [SDT]) VALUES (N'NV3', N'Test', 25, 1, NULL, NULL, NULL, N'876543')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P1', N'P101', N'rẻ', N'MTT1', N'MLP1')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P10', N'P402', N'đẹp', N'MTT1', N'MLP4')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P11', N'P403', N'rẻ', N'MTT1', N'MLP4')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P2', N'P102', N'rẻ', N'MTT1', N'MLP1')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P3', N'P103', N'rẻ', N'MTT1', N'MLP1')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P4', N'P201', N'rẻ', N'MTT2', N'MLP2')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P5', N'P202', N'rẻ', N'MTT1', N'MLP2')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P6', N'P203', N'rẻ', N'MTT1', N'MLP2')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P7', N'P302', N'rẻ', N'MTT1', N'MLP3')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P8', N'P303', N'rẻ', N'MTT1', N'MLP3')
GO
INSERT [dbo].[Phong] ([MaPhong], [TenPhong], [MoTaPhong], [MaTrangThai], [MaLoaiPhong]) VALUES (N'P9', N'P203', N'rẻ', N'MTT1', N'MLP3')
GO
INSERT [dbo].[Tai_Khoan] ([MaTaiKhoan], [UserName], [Password], [LoaiTaiKhoan], [PersonID]) VALUES (N'TK1', N'admin', N'1', N'LTK1', N'NV1')
GO
INSERT [dbo].[Tai_Khoan] ([MaTaiKhoan], [UserName], [Password], [LoaiTaiKhoan], [PersonID]) VALUES (N'TK3', N'ngoc123@gmail.com', N'123456', N'LTK3', N'65343245')
GO
INSERT [dbo].[Tai_Khoan] ([MaTaiKhoan], [UserName], [Password], [LoaiTaiKhoan], [PersonID]) VALUES (N'TK5', N'test@gmail.com', N'123456', N'LTK2', N'NV3')
GO
INSERT [dbo].[Tai_Khoan] ([MaTaiKhoan], [UserName], [Password], [LoaiTaiKhoan], [PersonID]) VALUES (N'TK6', N'khach1@gmacil.com', N'1', N'LTK3', N'3455464')
GO
INSERT [dbo].[Trang_Thai_Phong] ([MaTrangThai], [TenTrangThai]) VALUES (N'MTT1', N'Trống')
GO
INSERT [dbo].[Trang_Thai_Phong] ([MaTrangThai], [TenTrangThai]) VALUES (N'MTT2', N'Đang thuê')
GO
INSERT [dbo].[Trang_Thai_Phong] ([MaTrangThai], [TenTrangThai]) VALUES (N'MTT3', N'Đã đặt trước')
GO
INSERT [dbo].[Vai_Tro] ([MaVaiTro], [TenVaiTro]) VALUES (N'MVT1', N'quản lý')
GO
INSERT [dbo].[Vai_Tro] ([MaVaiTro], [TenVaiTro]) VALUES (N'MVT2', N'nhân viên')
GO
ALTER TABLE [dbo].[Order_Phong] ADD  DEFAULT ((0)) FOR [TrangThaiThanhToan]
GO
ALTER TABLE [dbo].[Hoa_Don]  WITH CHECK ADD  CONSTRAINT [FKHoa_Don624260] FOREIGN KEY([MaOrderPhong])
REFERENCES [dbo].[Order_Phong] ([MaOrderPhong])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Hoa_Don] CHECK CONSTRAINT [FKHoa_Don624260]
GO
ALTER TABLE [dbo].[Khach_Hang]  WITH CHECK ADD  CONSTRAINT [FKKhach_Hang279424] FOREIGN KEY([KhachHang_ID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Khach_Hang] CHECK CONSTRAINT [FKKhach_Hang279424]
GO
ALTER TABLE [dbo].[Nhan_Vien]  WITH CHECK ADD  CONSTRAINT [FKNhan_Vien605300] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Nhan_Vien] CHECK CONSTRAINT [FKNhan_Vien605300]
GO
ALTER TABLE [dbo].[Nhan_Vien]  WITH CHECK ADD  CONSTRAINT [FKNhan_Vien799741] FOREIGN KEY([MaVaiTro])
REFERENCES [dbo].[Vai_Tro] ([MaVaiTro])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Nhan_Vien] CHECK CONSTRAINT [FKNhan_Vien799741]
GO
ALTER TABLE [dbo].[Order_Phong]  WITH CHECK ADD  CONSTRAINT [FKOrder_Phon460975] FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Phong] CHECK CONSTRAINT [FKOrder_Phon460975]
GO
ALTER TABLE [dbo].[Order_Phong]  WITH CHECK ADD  CONSTRAINT [FKOrder_Phon746646] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Phong] CHECK CONSTRAINT [FKOrder_Phon746646]
GO
ALTER TABLE [dbo].[Order_Phong_Dich_Vu]  WITH CHECK ADD  CONSTRAINT [FKOrder_Phon17642] FOREIGN KEY([MaOrderPhong])
REFERENCES [dbo].[Order_Phong] ([MaOrderPhong])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Phong_Dich_Vu] CHECK CONSTRAINT [FKOrder_Phon17642]
GO
ALTER TABLE [dbo].[Order_Phong_Dich_Vu]  WITH CHECK ADD  CONSTRAINT [FKOrder_Phon597344] FOREIGN KEY([MaDichVu])
REFERENCES [dbo].[Dich_Vu] ([MaDichVu])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order_Phong_Dich_Vu] CHECK CONSTRAINT [FKOrder_Phon597344]
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FKPhong128242] FOREIGN KEY([MaTrangThai])
REFERENCES [dbo].[Trang_Thai_Phong] ([MaTrangThai])
GO
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FKPhong128242]
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FKPhong134689] FOREIGN KEY([MaLoaiPhong])
REFERENCES [dbo].[Loai_Phong] ([MaLoaiPhong])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FKPhong134689]
GO
ALTER TABLE [dbo].[Tai_Khoan]  WITH CHECK ADD  CONSTRAINT [FKTai_Khoan172310] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tai_Khoan] CHECK CONSTRAINT [FKTai_Khoan172310]
GO
ALTER TABLE [dbo].[Tai_Khoan]  WITH CHECK ADD  CONSTRAINT [FKTai_Khoan92928] FOREIGN KEY([LoaiTaiKhoan])
REFERENCES [dbo].[Loai_Tai_Khoan] ([MaLoaiTaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tai_Khoan] CHECK CONSTRAINT [FKTai_Khoan92928]
GO


-- Thêm cột NgayThanhToan vào bảng Hoa_Don
ALTER TABLE Hoa_Don
ADD NgayThanhToan DATETIME NULL;

-- Thêm cột PhuongThucThanhToan vào bảng Order_Phong
ALTER TABLE Order_Phong
ADD PhuongThucThanhToan NVARCHAR(50) NULL;

-- Đảm bảo cột TongTien đã tồn tại, nếu chưa thì thêm:
IF COL_LENGTH('Hoa_Don', 'TongTien') IS NULL
BEGIN
    ALTER TABLE Hoa_Don
    ADD TongTien DECIMAL(18,2) NULL;
END

-- Thêm NgayThanhToan vào Hoa_Don
IF COL_LENGTH('Hoa_Don', 'NgayThanhToan') IS NULL
BEGIN
    ALTER TABLE Hoa_Don
    ADD NgayThanhToan DATETIME NULL;
END

-- Thêm NgayThanhToan vào Order_Phong
IF COL_LENGTH('Order_Phong', 'NgayThanhToan') IS NULL
BEGIN
    ALTER TABLE Order_Phong
    ADD NgayThanhToan DATETIME NULL;
END
