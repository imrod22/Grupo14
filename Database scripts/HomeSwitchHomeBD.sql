USE [HomeSwitchHome]
GO
/****** Object:  Table [dbo].[ADMINISTRADOR]    Script Date: 5/6/2019 00:52:59 ******/
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
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 5/6/2019 00:52:59 ******/
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
/****** Object:  Table [dbo].[HOTSALE]    Script Date: 5/6/2019 00:52:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOTSALE](
	[IdHotSale] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[FechaDisponible] [date] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHotSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMAGEN]    Script Date: 5/6/2019 00:52:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMAGEN](
	[IdImagen] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NOVEDAD_PROPIEDAD]    Script Date: 5/6/2019 00:52:59 ******/
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
/****** Object:  Table [dbo].[PREMIUM]    Script Date: 5/6/2019 00:52:59 ******/
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
/****** Object:  Table [dbo].[PROPIEDAD]    Script Date: 5/6/2019 00:52:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROPIEDAD](
	[IdPropiedad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Pais] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPropiedad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RESERVA]    Script Date: 5/6/2019 00:52:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESERVA](
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[IdPropiedad] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SUBASTA]    Script Date: 5/6/2019 00:52:59 ******/
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
PRIMARY KEY CLUSTERED 
(
	[IdSubasta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 5/6/2019 00:52:59 ******/
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
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (1, 1, N'Imanol', N'Rodriguez', CAST(N'2000-03-12' AS Date), N'50 NRO 1432', N'DEBITO', N'GALICIA', 199922734, 32870500, NULL)
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (2, 2, N'Hernan', N'Leiva', CAST(N'1994-09-03' AS Date), N'172 NRO 990', N'EFECTIVO', N'NINGUNO', 0, 35990432, NULL)
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (3, 3, N'Tadeo', N'Velis', CAST(N'1998-01-12' AS Date), N'522 NRO 664', N'CREDITO', N'NACION', 231222235, 37882001, NULL)
GO
INSERT [dbo].[CLIENTE] ([IdCliente], [IdUsuario], [Nombre], [Apellido], [FechaDeNacimiento], [DomicioFiscal], [MedioDePago], [Banco], [CBU], [DNI], [Email]) VALUES (4, 4, N'Osvaldo', N'Mantecovich', CAST(N'1984-06-06' AS Date), N'34 NRO 112', N'DEBITO', N'ICBC', 299322133, 27376519, NULL)
GO
SET IDENTITY_INSERT [dbo].[CLIENTE] OFF
GO
SET IDENTITY_INSERT [dbo].[IMAGEN] ON 
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path]) VALUES (2, 1, N'/app-content/woodland_us.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path]) VALUES (5, 2, N'/app-content/adrede_egipto.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path]) VALUES (6, 3, N'/app-content/icelandhouse.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path]) VALUES (7, 4, N'/app-content/albuquerque_nm.jpg')
GO
INSERT [dbo].[IMAGEN] ([IdImagen], [IdPropiedad], [Path]) VALUES (8, 6, N'/app-content/madagascar_home.jpg')
GO
SET IDENTITY_INSERT [dbo].[IMAGEN] OFF
GO
SET IDENTITY_INSERT [dbo].[PREMIUM] ON 
GO
INSERT [dbo].[PREMIUM] ([IdPremium], [IdCliente], [Aceptado]) VALUES (1, 2, N'NO')
GO
SET IDENTITY_INSERT [dbo].[PREMIUM] OFF
GO
SET IDENTITY_INSERT [dbo].[PROPIEDAD] ON 
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais]) VALUES (1, N'Woodlandside', N'Esta lujosa propiedad inmobiliaria de 4 dormitorios y 5 baños es la casa de sus sueños en una comunidad de alto nivel', N'ESTADOS UNIDOS')
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais]) VALUES (2, N'Adrere Amellal', N'Adrere Amellal es un alojamiento ecológico en las afueras de Shali. No es un lugar en el que no encontrarás gente, pero si un paraje rural en un oasis remoto del desierto egipcio.', N'EGIPTO')
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais]) VALUES (3, N'Wordie House', N'No hay muchos sitios en los que hacer turismo en la Antártida, este es una antigua estación de investigacion que cuenta con la mayoria de los servicios', N'ANTARTIDA')
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais]) VALUES (4, N'Coswell Place', N'A Descripcion.', N'ESTADOS UNIDOS')
GO
INSERT [dbo].[PROPIEDAD] ([IdPropiedad], [Nombre], [Descripcion], [Pais]) VALUES (6, N'Isolate Island', N'Completar...', N'MADAGASCAR')
GO
SET IDENTITY_INSERT [dbo].[PROPIEDAD] OFF
GO
SET IDENTITY_INSERT [dbo].[SUBASTA] ON 
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (1, 1, 1, CAST(N'2019-05-15' AS Date), CAST(5000.00 AS Decimal(10, 2)), CAST(5100.00 AS Decimal(10, 2)), N'CANCELADO')
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (2, 2, 1, CAST(N'2019-06-01' AS Date), CAST(10000.00 AS Decimal(10, 2)), CAST(900001.00 AS Decimal(10, 2)), N'NUEVO')
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (3, 3, 2, CAST(N'2019-05-10' AS Date), CAST(2000.00 AS Decimal(10, 2)), CAST(2300.00 AS Decimal(10, 2)), N'CONFIRMADO')
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (6, 4, NULL, CAST(N'2019-07-31' AS Date), CAST(250.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'NUEVO')
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (7, 6, NULL, CAST(N'2019-05-31' AS Date), CAST(1200.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'CANCELADO')
GO
INSERT [dbo].[SUBASTA] ([IdSubasta], [IdPropiedad], [IdCliente], [FechaComienzo], [ValorMinimo], [ValorActual], [Estado]) VALUES (8, 6, NULL, CAST(N'2019-05-31' AS Date), CAST(55555.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'CANCELADO')
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
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
ALTER TABLE [dbo].[PREMIUM] ADD  DEFAULT ('NO') FOR [Aceptado]
GO
ALTER TABLE [dbo].[SUBASTA] ADD  DEFAULT ((0)) FOR [ValorActual]
GO
ALTER TABLE [dbo].[SUBASTA] ADD  DEFAULT ('NUEVO') FOR [Estado]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT ((0)) FOR [Login]
GO
ALTER TABLE [dbo].[ADMINISTRADOR]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[CLIENTE]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
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
