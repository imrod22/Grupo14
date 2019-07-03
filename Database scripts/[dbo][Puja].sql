CREATE TABLE [dbo].[PUJA](
	[IdPuja] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[IdSubasta] [int] NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Monto] [decimal](10, 2) NOT NULL,
)
GO
ALTER TABLE [dbo].[PUJA]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CLIENTE] ([IdCliente])
GO
ALTER TABLE [dbo].[PUJA]  WITH CHECK ADD FOREIGN KEY([IdSubasta])
REFERENCES [dbo].[SUBASTA] ([IdSubasta])
GO