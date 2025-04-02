USE  TRENDBEAUTY
GO
IF OBJECT_ID('[dbo].[DatosNegocio]', 'U') IS NOT NULL
   PRINT 'Tabla DatosNegocio ya existe'
ELSE
BEGIN
CREATE TABLE [dbo].[DatosNegocio](
	[IdNegocio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Nit] [int] NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Correo] [varchar](300) NULL,
	[FechaCreacion] [datetime] NULL DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[IdNegocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF OBJECT_ID('[dbo].[Usuario]', 'U') IS NOT NULL
   PRINT 'Tabla Usuario ya existe' 
ELSE
BEGIN
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](100) NULL,
	[Apellidos] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
	[Clave] [varchar](150) NULL,
	[Reestablecer] [bit] NULL DEFAULT 1,
	[Activo] [bit] NULL DEFAULT 1,
	[Rol] [varchar](100) NULL DEFAULT 'US',
	[FechaCreacion] [datetime] NULL DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF OBJECT_ID('[dbo].[Categorias]', 'U') IS NOT NULL
   PRINT 'Tabla Categorias ya existe'
ELSE
BEGIN
CREATE TABLE [dbo].[Categorias](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Activo] [bit] NULL DEFAULT 1,
	[FechaCreacion] [datetime] NULL DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

GO

IF OBJECT_ID('[dbo].[Productos]', 'U') IS NOT NULL
   PRINT 'Tabla Productos ya existe'
ELSE
BEGIN
CREATE TABLE [dbo].[Productos](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[Costo] [decimal](18, 2) NOT NULL,
	[FechaCompra] [datetime] NULL,
	[Observaciones] [varchar](4000) NULL,
	[Activo] [bit] NULL DEFAULT 1,
	[Stock] [int] NULL,
	[RutaImagen] [varchar](500) NULL,
	[NombreImagen] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarUSuario](
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Correo = @Correo)
	BEGIN
		INSERT INTO USUARIO(Nombres,Apellidos,Correo,Clave,Activo) VALUES
		(@Nombres,@Apellidos,@Correo,@Clave,@Activo)

		SET @Resultado = SCOPE_IDENTITY()

	END
	ELSE
		SET @Mensaje = 'El correo del usuario ya existe'
END;
GO
IF OBJECT_ID('[dbo].[sp_EditarUSuario]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EditarUSuario
GO
CREATE PROCEDURE [dbo].[sp_EditarUSuario](
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
	BEGIN
	UPDATE TOP(1) USUARIO SET
	Nombres = @Nombres,
	Apellidos = @Apellidos,
	Correo = @Correo,
	Activo = @Activo
	WHERE IdUsuario = @IdUsuario

		SET @Resultado = 1

	END
	ELSE
		SET @Mensaje = 'El correo del usuario ya existe'
END;

GO

IF OBJECT_ID('[dbo].[sp_RegistrarDatosNegocio]', 'P') IS NOT NULL
   DROP PROCEDURE sp_RegistrarDatosNegocio
GO
CREATE PROCEDURE [dbo].[sp_RegistrarDatosNegocio](
@Nombre varchar(100),
@Nit int,
@Direccion varchar(100),
@Correo varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM DatosNegocio WHERE Correo = @Correo)
	BEGIN
		INSERT INTO DatosNegocio(Nombre,Nit,Direccion,Correo) VALUES
		(@Nombre,@Nit,@Direccion,@Correo)

		SET @Resultado = SCOPE_IDENTITY()

	END
	ELSE
		SET @Mensaje = 'El correo para este negocio ya existe'
END;

GO
IF OBJECT_ID('[dbo].[sp_EditarDatosNegocio]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EditarDatosNegocio
GO
CREATE PROCEDURE [dbo].[sp_EditarDatosNegocio](
@IdNegocio int,
@Nombre varchar(100),
@Nit varchar(100),
@Direccion varchar(100),
@Correo varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM DatosNegocio WHERE IdNegocio = @IdNegocio AND Nombre != @Nombre)
	BEGIN
		UPDATE DatosNegocio SET
		Nombre = @Nombre,
		Nit = @Nit,
		Direccion = @Direccion,
		Correo = @Correo
		WHERE IdNegocio = @IdNegocio

		SET @Resultado = 1

	END
	ELSE
		SET @Mensaje = 'El Negocio ya existe'
END;
GO
IF OBJECT_ID('[dbo].[sp_RegistrarCategoria]', 'P') IS NOT NULL
   DROP PROCEDURE sp_RegistrarCategoria
GO
CREATE PROCEDURE [dbo].[sp_RegistrarCategoria](
@Nombre varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM CATEGORIAS WHERE Nombre = @Nombre)
	BEGIN
		INSERT INTO CATEGORIAS(Nombre,Activo) VALUES
		(@Nombre,@Activo)

		SET @Resultado = SCOPE_IDENTITY()

	END
	ELSE
		SET @Mensaje = 'Categoria ya existe'
END;

GO
IF OBJECT_ID('[dbo].[sp_EditarCategoria]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EditarCategoria
GO
Create PROCEDURE [dbo].[sp_EditarCategoria](
@IdCategoria Int,
@Nombre varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM CATEGORIAS WHERE Nombre = @Nombre AND IdCategoria != @IdCategoria)
	BEGIN
		UPDATE TOP (1) CATEGORIAS SET
		Nombre = @Nombre,
		Activo = @Activo
		WHERE IdCategoria = @IdCategoria

		SET @Resultado = 1

	END
	ELSE
		SET @Mensaje = 'Categoria ya existe'
END;

GO
IF OBJECT_ID('[dbo].[sp_EliminarCategoria]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EliminarCategoria
GO
create PROCEDURE [dbo].[sp_EliminarCategoria](
@IdCategoria Int,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM Productos P
				  INNER JOIN CATEGORIAS C ON C.IdCategoria = P.IdCategoria
				  WHERE P.IdCategoria = @IdCategoria)
	BEGIN
		DELETE TOP (1) FROM CATEGORIAS 
		WHERE IdCategoria = @IdCategoria

		SET @Resultado = 1

	END
	ELSE
		SET @Mensaje = 'La categoria se encuentra relacionada a un producto'
END;
GO
IF OBJECT_ID('[dbo].[Sp_ListarProductos]', 'P') IS NOT NULL
   DROP PROCEDURE Sp_ListarProductos
GO
CREATE PROC [dbo].[Sp_ListarProductos]
AS
BEGIN

 ---definimos el formato de la fecha --
 SET DATEFORMAT dmy;

SELECT P.IdProducto,p.Nombre,p.Descripcion,p.Costo,p.Stock,
p.FechaCompra,c.IdCategoria,c.Nombre[Categoria],p.Activo,p.Observaciones FROM PRODUCTOS p
INNER JOIN Categorias c ON P.IdCategoria = C.IdCategoria

END
GO
IF OBJECT_ID('[dbo].[sp_RegistrarProducto]', 'P') IS NOT NULL
   DROP PROCEDURE sp_RegistrarProducto
GO
CREATE PROCEDURE [dbo].[sp_RegistrarProducto](
@Nombre varchar(100),
@Descripcion varchar(100),
@IdCategoria varchar(100),
@Costo decimal (10,2),
@FechaCompra date,
@Stock int,
@Activo bit,
@Observaciones varchar(500) = '',
@IdUsuario int,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	DECLARE @IdTransaccion INT

BEGIN TRANSACTION OPERACION --LAS OPERACIONES QUE SE REALIZAN DENTRO DEL BEGIN TRANSACTION SE EJECUTAN TEMPORALMENTE
	IF NOT EXISTS(SELECT * FROM Productos WHERE Nombre = @Nombre)
	BEGIN
		INSERT INTO Productos(Nombre,Descripcion,IdCategoria,Costo,FechaCompra,Stock,Activo,Observaciones) VALUES
		(@Nombre,@Descripcion,@IdCategoria,@Costo,@FechaCompra,@Stock,@Activo,iif(@Observaciones = '', '',@Observaciones))

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El producto ya existe'

	COMMIT TRANSACTION OPERACION --CUANDO LLEGA AL COMMIT CONVIERTE LOS DATOS TEMPORALES YA A CAMBIOS DEFINITIVAMENTE 
END;

GO
IF OBJECT_ID('[dbo].[sp_EditarProducto]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EditarProducto
GO
CREATE PROCEDURE [dbo].[sp_EditarProducto](
@IdProducto int,
@Nombre varchar(100),
@Descripcion varchar(100),
@IdCategoria varchar(100),
@Costo decimal (10,2),
@FechaCompra date,
@Stock int,
@Activo bit,
@Observaciones varchar(500),
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM Productos WHERE IdProducto != @IdProducto AND Nombre = @Nombre)
	BEGIN
		UPDATE Productos SET
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		Costo = @Costo,
		FechaCompra = @FechaCompra,
		Stock = @Stock,
		Activo = @Activo,
		Observaciones = @Observaciones
		WHERE IdProducto = @IdProducto

		SET @Resultado = 1

	END
	ELSE
		SET @Mensaje = 'El producto ya existe'
END;

GO

IF OBJECT_ID('[dbo].[sp_EliminarProducto]', 'P') IS NOT NULL
   DROP PROCEDURE sp_EliminarProducto
GO
CREATE PROCEDURE [dbo].[sp_EliminarProducto](
@IdProducto int,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
BEGIN
		DELETE TOP(1) FROM Productos WHERE IdProducto = @IdProducto
END;