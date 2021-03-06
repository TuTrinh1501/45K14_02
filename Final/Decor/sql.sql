USE [master]
GO
/****** Object:  Database [nhaxinh]    Script Date: 3/5/2022 8:21:33 PM ******/
CREATE DATABASE [nhaxinh]

GO
ALTER DATABASE [nhaxinh] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [nhaxinh] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [nhaxinh] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [nhaxinh] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [nhaxinh] SET ARITHABORT OFF 
GO
ALTER DATABASE [nhaxinh] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [nhaxinh] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [nhaxinh] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [nhaxinh] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [nhaxinh] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [nhaxinh] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [nhaxinh] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [nhaxinh] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [nhaxinh] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [nhaxinh] SET  ENABLE_BROKER 
GO
ALTER DATABASE [nhaxinh] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [nhaxinh] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [nhaxinh] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [nhaxinh] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [nhaxinh] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [nhaxinh] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [nhaxinh] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [nhaxinh] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [nhaxinh] SET  MULTI_USER 
GO
ALTER DATABASE [nhaxinh] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [nhaxinh] SET DB_CHAINING OFF 
GO
ALTER DATABASE [nhaxinh] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [nhaxinh] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [nhaxinh] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [nhaxinh] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'nhaxinh', N'ON'
GO
ALTER DATABASE [nhaxinh] SET QUERY_STORE = OFF
GO
USE [nhaxinh]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_ID] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [nvarchar](255) NULL,
	[Image] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Category_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gioithieu] [nvarchar](max) NULL,
	[facebook] [nvarchar](max) NULL,
	[sdt] [nvarchar](50) NULL,
	[dienchi] [nvarchar](500) NULL,
	[hotro] [nvarchar](50) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Details]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Order_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Order_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Total] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[Created_Time] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Order_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product_ID] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [nvarchar](255) NULL,
	[Description] [ntext] NOT NULL,
	[Category_ID] [int] NOT NULL,
	[Price] [float] NULL,
	[Quantity] [int] NOT NULL,
	[Image] [varchar](255) NULL,
	[PercentOfDiscount] [float] NULL,
	[Status] [int] NULL,
	[User_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/5/2022 8:21:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](255) NOT NULL,
	[Last_Name] [varchar](255) NOT NULL,
	[Role_ID] [int] NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Phone] [varchar](24) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK__Users__206D91908F86B728] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Category_ID], [Category_Name], [Image]) VALUES (1, N'Phòng Ngủ', NULL)
INSERT [dbo].[Categories] ([Category_ID], [Category_Name], [Image]) VALUES (2, N'Phòng Khách', NULL)
INSERT [dbo].[Categories] ([Category_ID], [Category_Name], [Image]) VALUES (3, N'Phòng Bếp', NULL)
INSERT [dbo].[Categories] ([Category_ID], [Category_Name], [Image]) VALUES (4, N'Trang Trí', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([id], [gioithieu], [facebook], [sdt], [dienchi], [hotro]) VALUES (1, N'Yume Decor hoạt động với sứ mệnh tạo ra các công trình khác biệt và sáng tạo thể hiện được phong cách riêng của gia chủ', N'Yume Decor nè', N'0366175373', N'Trần quang diệu ', NULL)
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[Order_Details] ON 

INSERT [dbo].[Order_Details] ([Id], [Order_ID], [Product_ID], [Quantity]) VALUES (2, 1, 1, 1)
INSERT [dbo].[Order_Details] ([Id], [Order_ID], [Product_ID], [Quantity]) VALUES (5, 2, 1, 2)
SET IDENTITY_INSERT [dbo].[Order_Details] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Order_ID], [User_ID], [Total], [Status], [Created_Time]) VALUES (1, 2, 28490000, 0, CAST(N'2022-05-03T13:06:30.197' AS DateTime))
INSERT [dbo].[Orders] ([Order_ID], [User_ID], [Total], [Status], [Created_Time]) VALUES (2, 3, 0, 1, CAST(N'2022-05-03T16:40:59.650' AS DateTime))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Product_ID], [Product_Name], [Description], [Category_ID], [Price], [Quantity], [Image], [PercentOfDiscount], [Status], [User_ID]) VALUES (1, N'Giường', N'Lớn', 1, 2000000, 2, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlmVrChZdkrWefrC3G1Nm4G4wKpkkpqj-ZLw&usqp=CAU', 0, 1, 1)
INSERT [dbo].[Products] ([Product_ID], [Product_Name], [Description], [Category_ID], [Price], [Quantity], [Image], [PercentOfDiscount], [Status], [User_ID]) VALUES (2, N'Bàn', N'tròn', 2, 2000, 4, N'https://cf.shopee.vn/file/d2ea58d6ae7e066646fec08714dd178f', 0, 1, 1)
INSERT [dbo].[Products] ([Product_ID], [Product_Name], [Description], [Category_ID], [Price], [Quantity], [Image], [PercentOfDiscount], [Status], [User_ID]) VALUES (3, N'Ghế', N'aaaa', 3, 100000, 3, N'https://winchair.vn/wp-content/uploads/2019/10/Gh%E1%BA%BF-nh%E1%BB%B1a-Dori-m%C3%A0u-xanh-1.jpg', 0, 1, 1)
INSERT [dbo].[Products] ([Product_ID], [Product_Name], [Description], [Category_ID], [Price], [Quantity], [Image], [PercentOfDiscount], [Status], [User_ID]) VALUES (4, N'Gương', N'Đứng', 4, 150000, 6, N'https://baya.vn/media/catalog/product/cache/a87c63fd4e95b1606c03c9c7999fa76e/m/i/miramar_standing_mirror_baya_2000396.jpg', 0, 1, 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Role_ID], [Role_Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Role_ID], [Role_Name]) VALUES (2, N'Manager')
INSERT [dbo].[Roles] ([Role_ID], [Role_Name]) VALUES (3, N'Staff')
INSERT [dbo].[Roles] ([Role_ID], [Role_Name]) VALUES (4, N'Customer')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([User_ID], [First_Name], [Last_Name], [Role_ID], [Username], [Password], [Phone], [Address], [Email], [Status]) VALUES (1, N'admin', N'admin', 1, N'admin', N'1', N'0392221200', N'admin', N'admin@admin', 1)
INSERT [dbo].[Users] ([User_ID], [First_Name], [Last_Name], [Role_ID], [Username], [Password], [Phone], [Address], [Email], [Status]) VALUES (2, N'hi', N'Nguyen Duy 1', 4, N'long', N'123456', N'0392221200', N'Ha Noi 1', N'lonhvo@gmail.com', 1)
INSERT [dbo].[Users] ([User_ID], [First_Name], [Last_Name], [Role_ID], [Username], [Password], [Phone], [Address], [Email], [Status]) VALUES (3, N'Trinh', N'Ngô', 4, N'trinh', N'123456', N'0905123456', N'Ðà N?ng', N'trinhngothitu151@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [PercentOfDiscount]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Status__3B75D760]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Order_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrderDetail] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Products] ([Product_ID])
GO
ALTER TABLE [dbo].[Order_Details] CHECK CONSTRAINT [FK_ProductOrderDetail]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_UserOrder] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_UserOrder]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_CategoryProduct] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Categories] ([Category_ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_CategoryProduct]
GO
USE [master]
GO
ALTER DATABASE [nhaxinh] SET  READ_WRITE 
GO
