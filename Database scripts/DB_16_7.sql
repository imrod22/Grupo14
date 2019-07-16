USE [HomeSwitchHome]
GO
/****** Object:  Table [dbo].[ADMINISTRADOR]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMINISTRADOR](
	[IdAdmin] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAdmin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[FechaDeNacimiento] [date] NULL,
	[DomicioFiscal] [nvarchar](max) NULL,
	[MedioDePago] [nvarchar](max) NULL,
	[Banco] [nvarchar](max) NULL,
	[CBU] [int] NULL,
	[DNI] [int] NULL,
	[Email] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CREDITO_CLIENTE]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CREDITO_CLIENTE](
	[IdCreditoCliente] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Credito] [int] NOT NULL,
	[Anio] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCreditoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOTSALE]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOTSALE](
	[IdHotSale] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[FechaDisponible] [date] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHotSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMAGEN]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMAGEN](
	[IdImagen] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NOVEDAD_PROPIEDAD]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOVEDAD_PROPIEDAD](
	[IdNovedadPropiedad] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNovedadPropiedad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PREMIUM]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PREMIUM](
	[IdPremium] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[Aceptado] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPremium] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROPIEDAD]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROPIEDAD](
	[IdPropiedad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Pais] [nvarchar](max) NOT NULL,
	[Ciudad] [nvarchar](max) NULL,
	[Activa] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPropiedad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PUJA]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PUJA](
	[IdPuja] [int] IDENTITY(1,1) NOT NULL,
	[IdSubasta] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Monto] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPuja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVA]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVA](
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Credito] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SUBASTA]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SUBASTA](
	[IdSubasta] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[IdCliente] [int] NULL,
	[FechaComienzo] [date] NOT NULL,
	[ValorMinimo] [decimal](10, 2) NOT NULL,
	[ValorActual] [decimal](10, 2) NOT NULL,
	[Estado] [nvarchar](max) NULL,
	[FechaReserva] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSubasta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 16/7/2019 17:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Login] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ADMINISTRADOR] ON 
GO
INSERT [dbo].[ADMINISTRADOR] ([IdAdmin], [IdUsuario]) VALUES (1, 1)
GO
SET IDENTITY_INSERT [dbo].[ADMINISTRADOR] OFF
GO
SET IDENTITY_INSERT [dbo].[CLIENTE] ON 
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (1, 1, N'Imanol', N'Rodriguez', CAST(N'2000-03-12' AS Date), N'50 NRO 1432', N'DEBITO', N'GALICIA', 185363431, 32870500, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (2, 2, N'Hernan', N'Leiva', CAST(N'1994-09-03' AS Date), N'172 NRO 990', N'EFECTIVO', N'NINGUNO', 0, 35990432, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (3, 3, N'Tadeo', N'Velis', CAST(N'1998-01-12' AS Date), N'522 NRO 664', N'CREDITO', N'NACION', 694501798, 37882001, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (4, 4, N'Osvaldo', N'Mantecovich', CAST(N'1984-06-06' AS Date), N'34 NRO 112', N'DEBITO', N'ICBC', 299322133, 27376519, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (5, 5, N'Norberto', N'Luna', CAST(N'1983-09-13' AS Date), N'113 NRO 773', N'EFECTIVO', N'NINGUNO', 0, 15890768, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (6, 6, N'Tomas', N'Cazon', CAST(N'1978-02-05' AS Date), N'113 NRO 773', N'DEBITO', N'SANTANDER', 838886301, 12893768, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (7, 7, N'Amalia', N'Asconi', CAST(N'1990-01-12' AS Date), N'113 NRO 773', N'EFECTIVO', N'NINGUNO', 0, 90023123, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (8, 8, N'Yanela', N'Colman', CAST(N'1983-09-13' AS Date), N'113 NRO 773', N'CREDITO', N'HSBC', 231222235, 78922176, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (9, 9, N'German', N'Ruiz Diaz', CAST(N'1981-03-23' AS Date), N'113 NRO 773', N'CREDITO', N'GALICIA', 936912074, 28009003, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (10, 10, N'Laura', N'Aito', CAST(N'1963-11-19' AS Date), N'113 NRO 773', N'DEBITO', N'BBVA', 630726777, 45210060, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (11, 11, N'Gabriela', N'Trellio', CAST(N'1969-04-30' AS Date), N'113 NRO 773', N'DEBITO', N'NACION', 500087337, 11313091, N'imrod.nameless@gmail.com')
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (12, 12, N'Juan Martin', N'Laurin', CAST(N'2000-07-02' AS Date), N'113 NRO 773', N'EFECTIVO', N'NINGUNO', 0, 26091203, N'imrod.nameless@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[CLIENTE] OFF
GO
SET IDENTITY_INSERT [dbo].[CREDITO_CLIENTE] ON 
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (1, 2, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (2, 2, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (3, 3, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (4, 3, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (6, 4, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (7, 4, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (8, 2, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (9, 3, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (10, 4, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (41, 5, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (42, 5, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (43, 5, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (44, 6, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (45, 6, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (46, 6, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (47, 7, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (48, 7, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (49, 7, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (50, 8, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (51, 8, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (52, 8, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (53, 9, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (54, 9, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (55, 9, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (56, 10, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (57, 10, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (58, 10, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (59, 11, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (60, 11, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (61, 11, 2, 2021)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (62, 12, 2, 2019)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (63, 12, 2, 2020)
GO
INSERT [dbo].[CREDITO_CLIENTE] ([IdCreditoCliente], [IdCliente], [Credito], [Anio]) VALUES (64, 12, 2, 2021)
GO
SET IDENTITY_INSERT [dbo].[CREDITO_CLIENTE] OFF
GO
SET IDENTITY_INSERT [dbo].[HOTSALE] ON 
GO
INSERT [dbo].[HOTSALE] ([IdHotSale], [IdPropiedad], [FechaDisponible], [Precio], [Estado]) VALUES (2, 4, CAST(N'2020-01-30' AS Date), CAST(80.00 AS Decimal(10, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[HOTSALE] OFF
GO
SET IDENTITY_INSERT [dbo].[IMAGEN] ON 
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path], [Nombre]) VALUES (2, 1, N'/app-content/woodland_us.jpg', N'woodland_us.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path], [Nombre]) VALUES (5, 2, N'/app-content/adrede_egipto.jpg', N'adrede_egipto.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path], [Nombre]) VALUES (6, 3, N'/app-content/icelandhouse.jpg', N'icelandhouse.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path], [Nombre]) VALUES (7, 4, N'/app-content/albuquerque_nm.jpg', N'albuquerque_nm.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path], [Nombre]) VALUES (8, 6, N'/app-content/madagascar_home.jpg', N'madagascar_home.jpg')
GO
SET IDENTITY_INSERT [dbo].[IMAGEN] OFF
GO
SET IDENTITY_INSERT [dbo].[NOVEDAD_PROPIEDAD] ON 
GO
INSERT [dbo].[NOVEDAD_PROPIEDAD] ([IdNovedadPropiedad], [IdPropiedad], [IdCliente]) VALUES (1, 2, 7)
GO
SET IDENTITY_INSERT [dbo].[NOVEDAD_PROPIEDAD] OFF
GO
SET IDENTITY_INSERT [dbo].[PREMIUM] ON 
GO
INSERT [dbo].[PREMIUM] ([IdPremium], [IdCliente], [Aceptado]) VALUES (6, 2, N'SI')
GO
INSERT [dbo].[PREMIUM] ([IdPremium], [IdCliente], [Aceptado]) VALUES (7, 7, N'SI')
GO
SET IDENTITY_INSERT [dbo].[PREMIUM] OFF
GO
SET IDENTITY_INSERT [dbo].[PROPIEDAD] ON 
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais], [Ciudad], [Activa]) VALUES (1, N'Woodlandside', N'Esta lujosa propiedad inmobiliaria de 4 dormitorios y 5 baños es la casa de sus sueños en una comunidad de alto nivel', N'ESTADOS UNIDOS', N'Nueva York', 1)
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais], [Ciudad], [Activa]) VALUES (2, N'Adrere Amellal', N'Adrere Amellal es un alojamiento ecológico en las afueras de Shali. No es un lugar en el que no encontrarás gente, pero si un paraje rural en un oasis remoto del desierto egipcio.', N'EGIPTO', N'El Cairo', 1)
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais], [Ciudad], [Activa]) VALUES (3, N'Wordie House', N'No hay muchos sitios en los que hacer turismo en la Antártida, este es una antigua estación de investigacion que cuenta con la mayoria de los servicios', N'ISLANDIA', N'Njornk', 1)
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais], [Ciudad], [Activa]) VALUES (4, N'Coswell Place', N'Coswell city es una ciudad blablabla', N'ESTADOS UNIDOS', N'Reno', 1)
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais], [Ciudad], [Activa]) VALUES (6, N'Isolate Island', N'Completar...', N'MADAGASCAR', N'Anvennino', 1)
GO
SET IDENTITY_INSERT [dbo].[PROPIEDAD] OFF
GO
SET IDENTITY_INSERT [dbo].[SUBASTA] ON 
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado], [FechaReserva]) VALUES (1, 1, 3, CAST(N'2019-05-15' AS Date), CAST(5000.00 AS Decimal(10, 2)), CAST(5100.00 AS Decimal(10, 2)), N'NUEVO', CAST(N'2020-07-12' AS Date))
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado], [FechaReserva]) VALUES (2, 2, 3, CAST(N'2019-06-01' AS Date), CAST(10000.00 AS Decimal(10, 2)), CAST(900001.00 AS Decimal(10, 2)), N'NUEVO', CAST(N'2020-07-12' AS Date))
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado], [FechaReserva]) VALUES (3, 3, 2, CAST(N'2019-05-10' AS Date), CAST(2000.00 AS Decimal(10, 2)), CAST(2300.00 AS Decimal(10, 2)), N'CONFIRMADO', CAST(N'2020-07-12' AS Date))
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado], [FechaReserva]) VALUES (6, 4, NULL, CAST(N'2019-07-31' AS Date), CAST(300.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'NUEVO', CAST(N'2020-07-12' AS Date))
GO
SET IDENTITY_INSERT [dbo].[SUBASTA] OFF
GO
SET IDENTITY_INSERT [dbo].[USUARIO] ON 
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (1, N'imrod', N'imrod22', 1)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (2, N'hleiva', N'hernan12', 1)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (3, N'tadeov', N'tadeo21', 1)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (4, N'ruso', N'ruso77', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (5, N'nmoon', N'nmoon32', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (6, N'tomasc', N'tomas22', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (7, N'aazpetia', N'ani1992', 1)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (8, N'ycarlini', N'ycarlini00', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (9, N'gruiz', N'gruiz11', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (10, N'laita', N'laita33', 0)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (11, N'gtrilla', N'gtrilla10', 1)
GO
INSERT [dbo].[USUARIO] ([IdUsuario], [Usuario], [Password], [Login]) VALUES (12, N'jlauri', N'jlauri55', 0)
GO
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
ALTER TABLE [dbo].[HOTSALE] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PREMIUM] ADD  DEFAULT ('NO') FOR [Aceptado]
GO
ALTER TABLE [dbo].[SUBASTA] ADD  DEFAULT ((0)) FOR [ValorActual]
GO
ALTER TABLE [dbo].[SUBASTA] ADD  DEFAULT ('NUEVO') FOR [Estado]
GO
ALTER TABLE [dbo].[SUBASTA] ADD  DEFAULT (getdate()) FOR [FechaReserva]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT ((0)) FOR [Login]
GO
ALTER TABLE [dbo].[ADMINISTRADOR]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[CLIENTE]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[CREDITO_CLIENTE]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[HOTSALE]  WITH CHECK ADD FOREIGN KEY([IdPropiedad])
REFERENCES [dbo].[PROPIEDAD] ([IdPropiedad])
GO
ALTER TABLE [dbo].[IMAGEN]  WITH CHECK ADD FOREIGN KEY([IdPropiedad])
REFERENCES [dbo].[PROPIEDAD] ([IdPropiedad])
GO
ALTER TABLE [dbo].[NOVEDAD_PROPIEDAD]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[NOVEDAD_PROPIEDAD]  WITH CHECK ADD FOREIGN KEY([IdPropiedad])
REFERENCES [dbo].[PROPIEDAD] ([IdPropiedad])
GO
ALTER TABLE [dbo].[PREMIUM]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[PUJA]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[PUJA]  WITH CHECK ADD FOREIGN KEY([IdSubasta])
REFERENCES [dbo].[SUBASTA] ([IdSubasta])
GO
ALTER TABLE [dbo].[RESERVA]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[RESERVA]  WITH CHECK ADD FOREIGN KEY([IdPropiedad])
REFERENCES [dbo].[PROPIEDAD] ([IdPropiedad])
GO
ALTER TABLE [dbo].[SUBASTA]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[SUBASTA]  WITH CHECK ADD FOREIGN KEY([IdPropiedad])
REFERENCES [dbo].[PROPIEDAD] ([IdPropiedad])
GO
