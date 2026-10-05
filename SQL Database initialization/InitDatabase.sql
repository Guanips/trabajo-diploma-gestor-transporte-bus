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
        Nombre VARCHAR(100) NOT NULL UNIQUE,
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

-- ---------------------------------------------------------
-- MULTI-IDIOMA
-- Idioma: idiomas disponibles. Solo uno es el idioma por defecto (EsDefault = 1), que es
--         el idioma en el que se registran automaticamente las etiquetas nuevas.
-- Etiqueta: catalogo unico de textos traducibles. La Clave tiene la forma
--         'Formulario.Control' (o 'Formulario.Grilla.Columna'); los mensajes usan claves sin formulario.
-- Traduccion: texto de cada etiqueta en cada idioma (una fila por par etiqueta-idioma).
-- ---------------------------------------------------------
IF OBJECT_ID(N'dbo.Idioma', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Idioma (
        Codigo VARCHAR(5) NOT NULL,     -- Ej: 'ES', 'EN'
        Nombre NVARCHAR(50) NOT NULL,   -- Ej: 'Español', 'English'
        EsDefault BIT NOT NULL CONSTRAINT DF_Idioma_EsDefault DEFAULT 0,
        CONSTRAINT PK_Idioma PRIMARY KEY (Codigo)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UIX_Idioma_Default' AND object_id = OBJECT_ID(N'dbo.Idioma'))
BEGIN
    -- Garantiza que exista a lo sumo un idioma por defecto
    CREATE UNIQUE INDEX UIX_Idioma_Default ON dbo.Idioma(EsDefault) WHERE EsDefault = 1;
END

IF OBJECT_ID(N'dbo.Etiqueta', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Etiqueta (
        IdEtiqueta INT IDENTITY(1,1) NOT NULL,
        Clave VARCHAR(200) NOT NULL,    -- Ej: 'LoginUI.loginUIButtonIniciarSesion', 'msg_TituloError'
        Formulario VARCHAR(100) NULL,   -- Ej: 'LoginUI'. NULL para los mensajes
        FechaAlta DATETIME NOT NULL CONSTRAINT DF_Etiqueta_FechaAlta DEFAULT GETDATE(),
        CONSTRAINT PK_Etiqueta PRIMARY KEY (IdEtiqueta),
        CONSTRAINT UQ_Etiqueta_Clave UNIQUE (Clave)
    );
END

IF OBJECT_ID(N'dbo.Traduccion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Traduccion (
        IdEtiqueta INT NOT NULL,
        CodigoIdioma VARCHAR(5) NOT NULL,
        Texto NVARCHAR(MAX) NOT NULL,
        CONSTRAINT PK_Traduccion PRIMARY KEY (IdEtiqueta, CodigoIdioma),
        CONSTRAINT FK_Traduccion_Etiqueta FOREIGN KEY (IdEtiqueta) REFERENCES dbo.Etiqueta(IdEtiqueta) ON DELETE CASCADE,
        CONSTRAINT FK_Traduccion_Idioma FOREIGN KEY (CodigoIdioma) REFERENCES dbo.Idioma(Codigo)
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Usuario_Idioma')
BEGIN
    ALTER TABLE dbo.Usuario
        ADD CONSTRAINT FK_Usuario_Idioma FOREIGN KEY (Idioma) REFERENCES dbo.Idioma(Codigo);
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

IF OBJECT_ID(N'dbo.Chofer', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Chofer (
        num_chofer INT IDENTITY(0,1) NOT NULL,
        dni INT NOT NULL,
        nombreCompleto VARCHAR(100) NOT NULL,
        activo BIT NOT NULL DEFAULT 1,
        CONSTRAINT PK_Chofer PRIMARY KEY (num_chofer)
    );
END

IF OBJECT_ID(N'dbo.Interno', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Interno (
        num_interno INT IDENTITY(0,1) NOT NULL,
        patente VARCHAR(20) NOT NULL,
        modelo VARCHAR(50) NOT NULL,
        fechaIncorporacion DATE NOT NULL,
        disponible BIT NOT NULL,
        CONSTRAINT PK_Interno PRIMARY KEY (num_interno)
    );
END

IF OBJECT_ID(N'dbo.Cronograma') IS NULL
BEGIN
    CREATE TABLE dbo.Cronograma (
        id INT IDENTITY(0,1) NOT NULL,
        descripcion NVARCHAR(MAX) NOT NULL,
        fechaValidez DATE NOT NULL,
        horaInicio TIME NOT NULL,
        horaFin TIME NOT NULL,
        frecuenciaMinutos INT NOT NULL,
        tiempoDescansoMinutos INT NOT NULL,
        id_ruta VARCHAR(20) NOT NULL,
        CONSTRAINT PK_Cronograma PRIMARY KEY (id),
        CONSTRAINT FK_Cronograma_Ruta FOREIGN KEY (id_ruta) REFERENCES dbo.Ruta(id)
    );
END

IF OBJECT_ID(N'dbo.Salida') IS NULL
BEGIN
    CREATE TABLE dbo.Salida (
        id INT IDENTITY(0,1) NOT NULL,
        id_cronograma INT NOT NULL,
        num_interno INT,
        num_chofer INT,
        horaSalidaTeorica TIME NOT NULL,
        horaLlegadaTeorica TIME NOT NULL,
        horaSalidaReal TIME,
        horaLlegadaReal TIME,
        estaSuspendida BIT NOT NULL,
        motivoSuspension NVARCHAR(MAX),
        CONSTRAINT PK_Salida PRIMARY KEY (id),
        CONSTRAINT FK_Salida_Cronograma FOREIGN KEY (id_cronograma) REFERENCES dbo.Cronograma(id),
        CONSTRAINT FK_Salida_Interno FOREIGN KEY (num_interno) REFERENCES dbo.Interno(num_interno),
        CONSTRAINT FK_Salida_Chofer FOREIGN KEY (num_chofer) REFERENCES dbo.Chofer(num_Chofer)
    );
END

IF OBJECT_ID(N'dbo.CargaCombustible') IS NULL
BEGIN
    CREATE TABLE dbo.CargaCombustible (
        id INT IDENTITY(0,1) NOT NULL,
        num_interno INT NOT NULL,
        fechaHora DATETIME NOT NULL,
        litrosCargados DECIMAL(10,2) NOT NULL,
        precioPorLitro DECIMAL(10,2) NOT NULL,
        kilometrajeActual INT NOT NULL,
        anulada BIT NOT NULL,
        motivoAnulacion NVARCHAR(MAX),
        CONSTRAINT  PK_CargaCombustible PRIMARY KEY (id),
        CONSTRAINT FK_CargaCombustible_Interno FOREIGN KEY (num_interno) REFERENCES dbo.Interno(num_interno)
    );
END

IF OBJECT_ID(N'dbo.RevisionTaller') IS NULL
BEGIN
    CREATE TABLE dbo.RevisionTaller (
        id INT IDENTITY(0,1) NOT NULL,
        num_interno INT NOT NULL,
        fecha DATE NOT NULL,
        descripcion NVARCHAR(MAX) NOT NULL,
        reparacionRequerida BIT NOT NULL,
        CONSTRAINT  PK_RevisionTaller PRIMARY KEY (id),
        CONSTRAINT FK_RevisionTaller_Interno FOREIGN KEY (num_interno) REFERENCES dbo.Interno(num_interno)
    );
END

IF OBJECT_ID(N'dbo.OrdenReparacion') IS NULL
BEGIN
    CREATE TABLE dbo.OrdenReparacion (
        id INT IDENTITY(0,1) NOT NULL,
        id_revision INT NOT NULL,
        motivoReparacion NVARCHAR(MAX) NOT NULL,
        CONSTRAINT  PK_OrdenReparacion PRIMARY KEY (id),
        CONSTRAINT FK_OrdenReparacion_Revision FOREIGN KEY (id_revision) REFERENCES dbo.RevisionTaller(id)
    );
END

IF OBJECT_ID(N'dbo.DetalleOrdenReparacion') IS NULL
BEGIN
    CREATE TABLE dbo.DetalleOrdenReparacion (
        id INT IDENTITY(0,1) NOT NULL,
        id_ordenReparacion INT NOT NULL,
        insumo NVARCHAR(MAX) NOT NULL,
        cantidad INT NOT NULL,
        costoUnitario INT,
        CONSTRAINT  PK_DetalleOrdenReparacion PRIMARY KEY (id),
        CONSTRAINT FK_DetalleOrdenReparacion_OrdenReparacion FOREIGN KEY (id_ordenReparacion) REFERENCES dbo.OrdenReparacion(id)
    );
END

IF OBJECT_ID(N'dbo.Sancion') IS NULL
BEGIN
    CREATE TABLE dbo.Sancion (
        id INT IDENTITY(0,1) NOT NULL,
        num_chofer INT NOT NULL,
        fecha DATE NOT NULL,
        motivo NVARCHAR(MAX) NOT NULL,
        CONSTRAINT PK_Sancion PRIMARY KEY (id),
        CONSTRAINT FK_Sancion_Chofer FOREIGN KEY (num_chofer) REFERENCES dbo.Chofer(num_chofer)
    );
END