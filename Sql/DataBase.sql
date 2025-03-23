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