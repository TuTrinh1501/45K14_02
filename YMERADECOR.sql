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
									GhiChuKH nvarchar(50),
									primary key(MaKH),
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
									MaKH varchar(15),
									primary key(MaGH),
									foreign key (MaGH) references KhachHang(MaKH),
									)
--
--
create table DonHang(
									MaDH varchar(15) ,
									MaGH varchar(15),
									MaKH varchar(15),
									MaSP varchar(15),
									SoLuong int,
									NgayDatHang datetime,
									primary key(MaDH, MaSP),
									foreign key (MaKH) references KhachHang(MaKH),
									foreign key(MaSP) references SanPham(MaSP),
									foreign key(MaGH) references GioHang(MaGH)
									)
-- 
-- 
create table GioHangChiTiet(
									MaGH varchar(15),
									MaSP varchar(15),
									SoLuong int,
									primary key (MaGH, MaSP),
									foreign key (MaGH) references GioHang(MaGH),
									foreign key (MaSP) references SanPham(MaSP)
									)
--
--
--------------------------------------------------|
--					AUTO TĂNG MÃ				   |
--------------------------------------------------|
-- MÃ KHÁCH HÀNG
go
create function fNewMaKH ()
returns varchar(20)
as
begin
	declare @NewMaKH varchar(15), @count int

	set @count = (select count(MaKH) from KhachHang)
	if @count < 1
	begin
		set @NewMaKH = 'KH' + '1001'
	end
	else
	begin
		set @NewMaKH = (select max(right(MaKH,4))+1 from KHACHHANG)
		set @NewMaKH = 'KH' + @NewMaKH
	end
	return @NewMaKH
end
go
--
select dbo.fNewMaKH()
--
--
-- MÃ CỬA HÀNG
go
create function fNewMaCH ()
returns varchar(20)
as
begin
	declare @NewMaCH varchar(15), @count int

	set @count = (select count(MaCH) from CuaHang)
	if @count < 1
	begin
		set @NewMaCH = 'CH' + '1001'
	end
	else
	begin
		set @NewMaCH = (select max(right(MaCH,4))+1 from CuaHang)
		set @NewMaCH = 'CH' + @NewMaCH
	end
	return @NewMaCH
end
go
--
select dbo.fNewMaCH()
--
--
-- MÃ GIỎ HÀNG
go
create function fNewMaGH ()
returns varchar(20)
as
begin
	declare @NewMaGH varchar(15), @count int

	set @count = (select count(MaGH) from GioHang)
	if @count < 1
	begin
		set @NewMaGH = 'GH' + '1001'
	end
	else
	begin
		set @NewMaGH = (select max(right(MaGH,4))+1 from GioHang)
		set @NewMaGH = 'GH' + @NewMaGH
	end
	return @NewMaGH
end
go
--
select dbo.fNewMaGH()
--
--
-- MÃ SẢN PHẨM
go
create function fNewMaSP ()
returns varchar(20)
as
begin
	declare @NewMaSP varchar(15), @count int

	set @count = (select count(MaSP) from SanPham)
	if @count < 1
	begin
		set @NewMaSP = 'SP' + '1001'
	end
	else
	begin
		set @NewMaSP = (select max(right(MaSP,4))+1 from SanPham)
		set @NewMaSP = 'SP' + @NewMaSP
	end
	return @NewMaSP
end
go
--
select dbo.fNewMaSP()
--
--
-- MÃ ĐƠN HÀNG
go
create function fNewMaDH ()
returns varchar(20)
as
begin
	declare @NewMaDH varchar(15), @count int

	set @count = (select count(MaDH) from DonHang)
	if @count < 1
	begin
		set @NewMaDH = 'DH' + '1001'
	end
	else
	begin
		set @NewMaDH = (select max(right(MaDH,4))+1 from DonHang)
		set @NewMaDH = 'GH' + @NewMaDH
	end
	return @NewMaDH
end
go
--
select dbo.fNewMaDH()
--
--
--------------------------------------------------|
--						SẢN PHẨM					   |
--------------------------------------------------|
-- THÊM MỚI SẢN PHẨM
go
create proc spAddSanPham(@TenSP nvarchar(100),  @LoaiSP nvarchar(100), @SoLuong int, @DonGiaBan numeric(18), @GhiChuSP nvarchar(50))
as
begin
	declare @count int = 0
	set @count = (select count(TenSP) from SanPham where TenSP = @TenSP)
	if @count < 1
	begin
		insert into SanPham(MaSP, TenSP, LoaiSP, SoLuong, DonGiaBan, GhiChuSP) values (dbo.fNewMaSP(), @TenSP, @LoaiSP, @SoLuong, @DonGiaBan, @GhiChuSP)
	end
end
go 
--
exec spAddSanPham N'Móc khóa hình máy ảnh',  N'Móc khóa', '10', '30000', ''
--
select * from SanPham
--
-- CẬP NHẬT SẢN PHẨM 
go 
create proc spUpdateSanPham(@MaSP varchar(15), @TenSP nvarchar(100),  @LoaiSP nvarchar(100), @SoLuong int, @DonGiaBan numeric(18), @GhiChuSP nvarchar(50))
as
begin
	Update SanPham
	set TenSP = @TenSP, LoaiSP = @LoaiSP, SoLuong = @SoLuong , DonGiaBan = @DonGiaBan , GhiChuSP = @GhiChuSP
	where MaSP = @MaSP
end
go
--
exec spUpdateSanPham 'SP1001', N'Móc khóa hình máy ảnh',  N'Móc khóa', '5', '40000', ''
--
-- XÓA SẢN PHẨM
go
create trigger tgDeleteSanPham
on SanPham
instead of delete 
as
begin
	update SanPham
	set SoLuong = '0'
end
go
--
go
create proc spDeleteSanPham (@MaSP varchar(15))
as
begin
	delete SanPham where MaSP = @MaSP
end
go
--
exec spDeleteSanPham 'SP1001'
--
--
-- XEM SẢN PHẨM
go
create proc spShowSanPham
as
begin
	select MaSP as N'Mã sản phẩm', TenSP as N'Tên sản phẩm', LoaiSP as N'Loại sản phẩm', SoLuong as N'Số lượng', DonGiaBan as N'Đơn giá bán', GhiChuSP as N'Ghi chú'
	from SanPham
end
go
--
exec spShowSanPham
--
--
--TÌM KIẾM SẢN PHẨM (THEO TÊN)
--
--
--------------------------------------------------|
--						CỬA HÀNG					   |
--------------------------------------------------|
-- THÊM CỬA HÀNG
go
create proc spAddCuaHang(@DiaChiCH nvarchar(200), @SDTCH varchar(13), @MaThueCH varchar(20), @STKCH varchar(20), @TenNganHangCH nvarchar(100), @GhiChuCH nvarchar(50))
as
begin
	declare @count int = 0
	set @count = (select count(SDTCH) from CuaHang where SDTCH = @SDTCH)
	if @count < 1
	begin
		insert into CuaHang(MaCH, DiaChiCH, SDTCH, MaThueCH, STKCH, TenNganHangCH, GhiChuCH)
		values (dbo.fNewMaCH(), @DiaChiCH, @SDTCH, @MaThueCH, @STKCH, @TenNganHangCH, @GhiChuCH)
	end
end
go 
--
exec spAddCuaHang N'71 Ngũ Hành Sơn - Đà Nẵng', '0367123558', '12345678910', '191121514109', N'Agribank Đà Nẵng', '' 
--
select * from CuaHang
--
-- CẬP NHẬT CỬA HÀNG
go
create proc spUpdateCuaHang (@MaCH varchar(15), @DiaChiCH nvarchar(200), @SDTCH varchar(13), @MaThueCH varchar(20), @STKCH varchar(20), @TenNganHangCH nvarchar(100), @GhiChuCH nvarchar(50))
as
begin
	update CuaHang
	set DiaChiCH = @DiaChiCH, SDTCH = @SDTCH, MaThueCH = @MaThueCH, STKCH = @STKCH, TenNganHangCH = @TenNganHangCH, GhiChuCH = @GhiChuCH
	where MaCH = @MaCH
end
go
--
exec spUpdateCuaHang 'CH1001', N'Ngũ Hành Sơn - Đà Nẵng', '0367123358', '92345678910', '991121514109', N'Agribank Đà Nẵng', '' 
--
--  XÓA CỬA HÀNG
go
create trigger tgDeleteCuaHang
on CuaHang
instead of delete 
as
begin
	update CuaHang
	set GhiChuCH = N'Đã xóa'
end
go
--
go
create proc spDeleteCuaHang (@MaCH varchar(15))
as
begin
	delete CuaHang where MaCH = @MaCH
end
go
--
exec spDeleteCuaHang 'CH1001'
--
--
--HIỂN THỊ CỬA HÀNG
go
create proc spShowCuaHang
as
begin
	select MaCH as N'Mã cửa hàng', DiaChiCH as N'Địa chỉ cửa hàng', SDTCH as N'Số điện thoại cửa hàng', MaThueCH as N'Mã thuế', STKCH as N'Số tài khoản cửa hàng', TenNganHangCH as N'Tên ngân hàng', GhiChuCH as N'Ghi chú'
	from CuaHang
end
go
--
exec spShowCuaHang
--
--
--------------------------------------------------|
--						GIỎ HÀNG 					   |
--------------------------------------------------|
-- THÊM GIỎ HÀNG
go
create proc spAddGioHang (@MaKH varchar(15))
as
begin
	insert into GioHang(MaGH, MaKH) values (dbo.fNewMaGH(), @MaKH)
end
go
--
exec spAddGioHang 'KH1001'
--
--
-- CẬP NHẬT GIỎ HÀNG
go 
create proc spUpdateGioHang (@MaGH varchar(15), @MaKH varchar(15))
as
begin
	update GioHang
	set MaKH = @MaKH
	where MaGH = @MaGH
end
go
--
exec spUpdateGioHang 'GH1001', 'KH1001', '5'
--
select * from GioHang
--
-- XÓA GIỎ HÀNG
go
create proc spDeleteGioHang (@MaGH varchar(15))
as
begin
	begin transaction

	delete GioHangChiTiet where MaGH = @MaGH
	delete GioHang where MaGH = @MaGH

	if @@ROWCOUNT > 0
	begin
		commit transaction
	end
	else
	begin
		rollback transaction
	end
end
go
--
exec spDeleteGioHang 'GH1001'
--
--
--	HIỂN THỊ GIỎ HÀNG
go
create proc spShowGioHang
as
begin
	select GioHang.MaGH as N'Mã giỏ hàng', GioHang.MaKH as N'Mã khách hàng', TenKH as N'Tên khách hàng'
	from GioHang  join KhachHang on GioHang.MaKH = KhachHang.MaKH
end
go
--
exec spShowGioHang
--
--
--------------------------------------------------|
--			    GIỎ HÀNG CHI TIẾT			   |
--------------------------------------------------|
--
--THÊM GIỎ HÀNG CHI TIẾT
go
create proc spAddGioHangChiTiet(@MaGH varchar(15), @MaSP varchar(15), @SoLuong int)
as
begin
	insert into GioHangChiTiet(MaGH, MaSP, SoLuong) values (@MaGH, @MaSP, @SoLuong)
end
go
--
exec spAddGioHangChiTiet  'GH1001', 'SP1001','3'
--
--
--CẬP NHẬT GIỎ HÀNG CHI TIẾT
go
create proc spUpdateGioHangChiTiet(@MaGH varchar(15), @MaSP varchar(15), @SoLuong int)
as
begin
	update GioHangChiTiet
	set SoLuong = @SoLuong
	where MaGH = @MaGH and MaSP = @MaSP
end
go
--
exec spUpdateGioHangChiTiet
--
--
--XÓA GIỎ HÀNG CHI TIẾT
go
create proc spDeleteGioHangChiTiet (@MaGH varchar(15), @MaSP varchar(15))
as
begin
	delete GioHangChiTiet where MaGH = @MaGH and MaSP = @MaSP
end
go
--
exec spDeleteGioHangChiTiet
--
--
--HIỂN THỊ GIỎ HÀNG CHI TIẾT
go
create proc spShowGioHangChiTiet 
as
begin
	select MaGH as N'Mã giỏ hàng', GioHangChiTiet.MaSP as N'Mã sản phẩm', SanPham.TenSP as N'Tên sản phẩm', GioHangChiTiet.SoLuong as N'Số lượng', (SanPham.DonGiaBan * GioHangChiTiet.SoLuong) as N'Thành tiền'
	from GioHangChiTiet join SanPham on GioHangChiTiet.MaSP = SanPham.MaSP
end
go
--
exec spShowGioHangChiTiet
--
--
--------------------------------------------------|
--						TÀI KHOẢN					   |
--------------------------------------------------|
--THÊM TÀI KHOẢN (lúc đăng ký)
go
create proc spAddTaiKhoan(@Tendangnhap varchar(50),
									@Matkhau varchar(50)--,
									--@Loaitaikhoan bit default 1, --0:admin ; 1: khách hàng
									--@GhiChuTK nvarchar(50)
									)
as
begin
	insert into TaiKhoan(Tendangnhap, Matkhau) values (@Tendangnhap, @Matkhau)
end
go
--
exec spAddTaiKhoan 'hungnguyen', '1'
--
--
--CẬP NHẬT TÀI KHOẢN (khách hàng)
go
create proc spUpdateTaiKhoan(@Tendangnhap varchar(50),
									@Matkhau varchar(50))
as
begin
	update TaiKhoan
	set Matkhau = @Matkhau
	where Tendangnhap = @Tendangnhap
end
go
--
exec spUpdateTaiKhoan 'hungnguyen', '11'
--
--
--XÓA TÀI KHOẢN (khách hàng/admin)
go
create trigger tgDeleteTaiKhoan
on TaiKhoan
instead of Delete
as
begin
	update TaiKhoan
	set GhiChuTK = N'Đã xóa'
	where Tendangnhap = (select Tendangnhap from deleted)
end
go
----
go
create proc spDeleteTaiKhoan(@Tendangnhap varchar(50))
as
begin
	delete TaiKhoan where Tendangnhap = @Tendangnhap
end
go
--
exec spDeleteTaiKhoan 'hungnguyen'
--
--
--THÊM TÀI KHOẢN(trong mục admin)
go
create proc spAddTaiKhoanAdmin(@Tendangnhap varchar(50),
									@Matkhau varchar(50),
									@Loaitaikhoan bit--, --0:admin ; 1: khách hàng
									--@GhiChuTK nvarchar(50)
									)
as
begin
	insert into TaiKhoan(Tendangnhap, Matkhau, Loaitaikhoan) values (@Tendangnhap, @Matkhau, @Loaitaikhoan)
end
go
--
exec spAddTaiKhoanAdmin 'admin', '1', 0
--
--
--CẬP NHẬT TÀI KHOẢN(admin)
go
create proc spUpdateTaiKhoanAdmin(@Tendangnhap varchar(50),
									@Matkhau varchar(50),
									@Loaitaikhoan bit, --0:admin ; 1: khách hàng
									@GhiChuTK nvarchar(50)
									)
as
begin
	update TaiKhoan
	set Matkhau = @Matkhau, Loaitaikhoan = @Loaitaikhoan, GhiChuTK = @GhiChuTK
	where Tendangnhap = @Tendangnhap
end
go
--
exec spUpdateTaiKhoanAdmin 'admin', 'admin', 0,''
--
--
--HIỂN THỊ THÔNG TIN TÀI KHOẢN (admin)
go
create proc spShowTaiKhoan
as
begin
		select Tendangnhap as N'Tên đăng nhập', Matkhau as N'Mật khẩu', (case when Loaitaikhoan = 1 then N'Khách hàng' 
																															else N'Admin' end) as N'Loại tài khoản', GhiChuTK as N'Ghi chú'
		from TaiKhoan
end
go
--
exec spShowTaiKhoan
--
--
--------------------------------------------------|
--						KHÁCH HÀNG				   |
--------------------------------------------------|
--THÊM KHÁCH HÀNG
go
create proc spAddKhachHang (@MaGH varchar(15),
									@TenKH nvarchar(100),
									@Tendangnhap varchar(50),
									@SDTKH varchar(13),
									@GioiTinhKH bit, -- 0:nam ; 1: nữ
									@DiaChiKH nvarchar(200),
									@GhiChuKH nvarchar(50))
as 
begin
	declare @count int = 0
	set @count = (select count(SDTKH) from KHACHHANG where SDTKH = @SDTKH)
	if @count < 1
	begin
		insert into KhachHang values (dbo.fNewMaKH(), @MaGH, @TenKH, @Tendangnhap, @SDTKH, @GioiTinhKH, @DiaChiKH, @GhiChuKH)
	end
end
go
--
exec spAddKhachHang 'GH1001', N'Hùng Nguyễn', 'hungnguyen', '0367123568', 0, N'Hà Tĩnh', ''
--
--CẬP NHẬT KHÁCH HÀNG (khách hàng)
go
create proc spUpdateKhachHang (@MaKH varchar(15),
									--@MaGH varchar(15),
									@TenKH nvarchar(100),
									--@Tendangnhap varchar(50),
									@SDTKH varchar(13),
									@GioiTinhKH bit, -- 0:nam ; 1: nữ
									@DiaChiKH nvarchar(200))
as 
begin
	declare @count int = 0
	set @count = (select count(SDTKH) from KHACHHANG where SDTKH = @SDTKH)
	if @count < 1
	begin
		update KhachHang
		set TenKH = @TenKH, SDTKH = @SDTKH, GioiTinhKH = @GioiTinhKH, DiaChiKH = @DiaChiKH
		where MaKH = @MaKH
	end
end
go
--
exec spUpdateKhachHang
--
--
--XÓA KHÁCH HÀNG (admin)
go
create trigger tgDeleteKhachHang
on KhachHang
instead of delete
as
begin
	update KhachHang
	set GhiChuKH = N'Đã xóa'
	where MaKH = (select MaKH from deleted)
end
go
--
go
create proc spDeleteKhachHang (@MaKH varchar(15))
as
begin
	delete KhachHang where MaKH = @MaKH
end
go
--
exec spDeleteKhachHang
--
--
--HIỂN THỊ KHÁCH HÀNG(admin)
go
create proc spShowKhachHang
as
begin
	select MaKH as N'Mã khách hàng', MaGH as N'Mã giỏ hàng', TenKH as N'Tên khách hàng', Tendangnhap as N'Tên đăng nhập', SDTKH as N'Số điện thoại',
				(case when GioiTinhKH = 0 then N'Nam' 
						else N'Nữ'
						end) as N'Giới tính', GhiChuKH as N'Ghi chú'
	from KhachHang
end
go
--
exec spShowKhachHang

