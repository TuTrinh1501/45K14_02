create database YUMERADECOR
use yumeradecor
--
--
create table TaiKhoan(
									Tendangnhap varchar(50),
									Matkhau varchar(50),
									Loaitaikhoan bit default 1, --0:admin ; 1: khách hàng
									GhiChuTK nvarchar(50),
									primary key(Tendangnhap)
									)
--
--
create table KhachHang(
									MaKH varchar(15),
									MaGH varchar(15),
									TenKH nvarchar(100),
									Tendangnhap varchar(50),
									SDTKH varchar(13),
									GioiTinhKH bit, -- 0:nam ; 1: nữ
									DiaChiKH nvarchar(200),
									primary key (MaKH, MaGH),
									foreign key (Tendangnhap) references TaiKhoan(Tendangnhap)
									)
--
--
create table CuaHang(
									MaCH varchar(15),
									DiaChiCH nvarchar(200),
									SDTCH varchar(13),
									MaThueCH varchar(20),
									STKCH varchar(20),
									TenNganHangCH nvarchar(100),
									GhiChuCH nvarchar(50),
									primary key(MaCH)
									)
--
--
create table SanPham(
									MaSP varchar(15),
									TenSP nvarchar(100),
									LoaiSP nvarchar(100),
									SoLuong int,
									DonGiaBan numeric(18),
									GhiChuSP nvarchar(50),
									primary key (MaSP)
									)
--
--
create table GioHang(
									MaGH varchar(15) ,
									MaSP varchar(15),
									SoLuong int,
									primary key (MaGH, MaSP),
									foreign key (MaGH) references KhachHang(MaGH),
									foreign key (MaSP) references SanPham(MaSP)
									)
--
--
create table DonHang(
									MaDH varchar(15) ,
									MaKH varchar(15),
									MaSP varchar(15),
									SoLuong int,
									NgayDatHang datetime,
									primary key (MaDH, MaSP),
									foreign key (MaKH) references KhachHang(MaKH),
									foreign key(MaSP) references SanPham(MaSP)
									)
--
--


