USE master;
GO

IF DB_ID(N'GestorTransporteCG') IS NULL
BEGIN
    CREATE DATABASE GestorTransporteCG;
END
GO

USE GestorTransporteCG;
GO

-- =========================================================
-- BLOQUE 1: ESTRUCTURA DE LA BASE DE DATOS
-- =========================================================

IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuario (
        ID UNIQUEIDENTIFIER PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        NumTelefono NVARCHAR(20) NOT NULL,
        EstaBloqueado BIT NOT NULL,
        Idioma VARCHAR(5) NOT NULL DEFAULT 'ES',
        IntentosFallidos INT NOT NULL DEFAULT 0,
        DVH NVARCHAR(256) NULL
    );
END

IF OBJECT_ID(N'dbo.DVV', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DVV (
        NombreTabla NVARCHAR(50) PRIMARY KEY,
        ValorHash NVARCHAR(100) NOT NULL
    );
END

IF OBJECT_ID(N'dbo.Bitacora', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Bitacora (
        ID INT PRIMARY KEY IDENTITY(0,1),
        Username NVARCHAR(50) NOT NULL,
        Fecha DATETIME NOT NULL,
        Accion NVARCHAR(50) NOT NULL
    );
END

IF OBJECT_ID(N'dbo.Permiso', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Permiso (
        ID INT PRIMARY KEY IDENTITY(0,1),
        Nombre VARCHAR(30) NOT NULL UNIQUE,
        EsPerfil BIT NOT NULL
    );
END

IF OBJECT_ID(N'dbo.PermisoRelacion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PermisoRelacion (
        ID_Padre INT NOT NULL,
        ID_Hijo INT NOT NULL,
        PRIMARY KEY (ID_Padre, ID_Hijo),
        CONSTRAINT FK_PermisoRelacion_Padre FOREIGN KEY (ID_Padre) REFERENCES dbo.Permiso(ID),
        CONSTRAINT FK_PermisoRelacion_Hijo FOREIGN KEY (ID_Hijo) REFERENCES dbo.Permiso(ID)
    );
END


IF OBJECT_ID(N'dbo.PerfilUsuario', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PerfilUsuario (
        ID_Usuario UNIQUEIDENTIFIER,
        ID_Perfil INT,
        PRIMARY KEY (ID_Usuario, ID_Perfil),
        CONSTRAINT FK_PerfilUsuarioUsuario FOREIGN KEY (ID_Usuario) REFERENCES dbo.Usuario(ID),
        CONSTRAINT FK_PerfilUsuarioPerfil FOREIGN KEY (ID_Perfil) REFERENCES dbo.Permiso(ID)
    );
END

IF OBJECT_ID(N'dbo.Idioma', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Idioma (
        Codigo VARCHAR(5) NOT NULL,   -- Ej: 'ES', 'EN'
        Nombre VARCHAR(50) NOT NULL,  -- Ej: 'Español', 'English'
        CONSTRAINT PK_Idioma PRIMARY KEY (Codigo)
    );
END

IF OBJECT_ID(N'dbo.Traduccion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Traduccion (
        IdTraduccion INT IDENTITY(1,1) NOT NULL,
        CodigoIdioma VARCHAR(5) NOT NULL,
        KeyEtiqueta VARCHAR(100) NOT NULL, -- El nombre del control (Ej: loginUILabelUsername)
        Texto NVARCHAR(MAX) NOT NULL,      -- El texto a mostrar (Ej: 'Nombre de usuario')
        CONSTRAINT PK_Traduccion PRIMARY KEY (IdTraduccion),
        CONSTRAINT FK_Traduccion_Idioma FOREIGN KEY (CodigoIdioma) REFERENCES dbo.Idioma(Codigo)
    );
END

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'UIX_Idioma_Etiqueta'
      AND object_id = OBJECT_ID(N'dbo.Traduccion')
)
BEGIN
    CREATE UNIQUE INDEX UIX_Idioma_Etiqueta ON dbo.Traduccion(CodigoIdioma, KeyEtiqueta);
END

IF OBJECT_ID(N'dbo.HistorialUsuario', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HistorialUsuario (
        ID INT PRIMARY KEY IDENTITY(0,1),
        ID_Usuario UNIQUEIDENTIFIER NOT NULL,
        Email VARCHAR(100),
        NumTelefono VARCHAR(20),
        Fecha DATETIME NOT NULL,
        CONSTRAINT FK_HistorialUsuarioUsuario FOREIGN KEY (ID_Usuario) REFERENCES dbo.Usuario(ID)
    );
END

GO

IF OBJECT_ID(N'dbo.Parada', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Parada (
        id VARCHAR(20) PRIMARY KEY,
        descripcion VARCHAR(255) NOT NULL,
        localidad VARCHAR(200) NOT NULL,
        direccion VARCHAR(200) NOT NULL,
        habilitada BIT NOT NULL
    );
END

IF OBJECT_ID(N'dbo.Ruta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ruta (
        id VARCHAR(20) PRIMARY KEY,
        descripcion VARCHAR(255) NOT NULL,
        sentido VARCHAR(20) NOT NULL,
        distanciaTotalKM INT NOT NULL,
        tiempoEstimadoMin INT NOT NULL
    );
END

IF OBJECT_ID(N'dbo.Recorrido_Ruta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Recorrido_Ruta (
        id_ruta VARCHAR(20) NOT NULL,
        id_parada VARCHAR(20) NOT NULL,
        orden INT NOT NULL,
        CONSTRAINT PK_recorrido_ruta PRIMARY KEY (id_ruta, id_parada),
        CONSTRAINT FK_Ruta_Recorrido FOREIGN KEY (id_ruta) REFERENCES dbo.Ruta(id),
        CONSTRAINT FK_Parada_Ruta FOREIGN KEY (id_parada) REFERENCES dbo.Parada(id)
    );
END
