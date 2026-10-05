USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: Multi-idioma (Idioma, Etiqueta y Traduccion)
-- =========================================================

-- Tipos tabla para enviar lotes desde la aplicacion en un solo viaje a la base
IF TYPE_ID(N'dbo.EtiquetaTablaTipo') IS NULL
BEGIN
	CREATE TYPE dbo.EtiquetaTablaTipo AS TABLE (
		Clave VARCHAR(200) NOT NULL,
		Formulario VARCHAR(100) NULL,
		Texto NVARCHAR(MAX) NOT NULL
	);
END
GO

IF TYPE_ID(N'dbo.TraduccionTablaTipo') IS NULL
BEGIN
	CREATE TYPE dbo.TraduccionTablaTipo AS TABLE (
		Clave VARCHAR(200) NOT NULL,
		Texto NVARCHAR(MAX) NULL
	);
END
GO

-- ---------------------------------------------------------
-- IDIOMA
-- ---------------------------------------------------------

-- Devuelve los idiomas con su avance de traduccion (cuantas etiquetas tienen texto propio)
CREATE OR ALTER PROCEDURE usp_Idioma_GetAll
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @totalEtiquetas INT = (SELECT COUNT(*) FROM dbo.Etiqueta);

	SELECT
		i.Codigo,
		i.Nombre,
		i.EsDefault,
		@totalEtiquetas AS TotalEtiquetas,
		(SELECT COUNT(*) FROM dbo.Traduccion t WHERE t.CodigoIdioma = i.Codigo) AS EtiquetasTraducidas
	FROM dbo.Idioma i
	ORDER BY i.EsDefault DESC, i.Nombre;
END
GO

CREATE OR ALTER PROCEDURE usp_Idioma_Insert
	@Codigo VARCHAR(5),
	@Nombre NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.Idioma WHERE Codigo = @Codigo)
		THROW 50001, 'El código de idioma ya existe.', 1;

	INSERT INTO dbo.Idioma (Codigo, Nombre, EsDefault) VALUES (@Codigo, @Nombre, 0);
END
GO

CREATE OR ALTER PROCEDURE usp_Idioma_Update
	@Codigo VARCHAR(5),
	@Nombre NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Idioma SET Nombre = @Nombre WHERE Codigo = @Codigo;
END
GO

-- ---------------------------------------------------------
-- ETIQUETA
-- ---------------------------------------------------------

CREATE OR ALTER PROCEDURE usp_Etiqueta_GetClaves
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Clave FROM dbo.Etiqueta;
END
GO

-- Autodeteccion: registra las etiquetas que todavia no existen y guarda su texto
-- (el que trae la aplicacion) como traduccion del idioma por defecto.
-- Las etiquetas que ya existen se ignoran, por lo que nunca pisa textos editados por el usuario.
CREATE OR ALTER PROCEDURE usp_Etiqueta_RegistrarFaltantes
	@Etiquetas dbo.EtiquetaTablaTipo READONLY
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @idiomaDefault VARCHAR(5) = (SELECT Codigo FROM dbo.Idioma WHERE EsDefault = 1);
	IF @idiomaDefault IS NULL
		THROW 50002, 'No hay un idioma por defecto configurado.', 1;

	BEGIN TRY
		BEGIN TRANSACTION;

		DECLARE @nuevas TABLE (IdEtiqueta INT, Clave VARCHAR(200));

		-- El lote puede traer claves repetidas: se toma una sola por clave.
		-- UPDLOCK/HOLDLOCK evita que dos instancias de la aplicacion inserten la misma clave a la vez.
		INSERT INTO dbo.Etiqueta (Clave, Formulario)
		OUTPUT inserted.IdEtiqueta, inserted.Clave INTO @nuevas (IdEtiqueta, Clave)
		SELECT src.Clave, MAX(src.Formulario)
		FROM @Etiquetas src
		WHERE NOT EXISTS (SELECT 1 FROM dbo.Etiqueta e WITH (UPDLOCK, HOLDLOCK) WHERE e.Clave = src.Clave)
		GROUP BY src.Clave;

		INSERT INTO dbo.Traduccion (IdEtiqueta, CodigoIdioma, Texto)
		SELECT n.IdEtiqueta, @idiomaDefault, MAX(src.Texto)
		FROM @nuevas n
		INNER JOIN @Etiquetas src ON src.Clave = n.Clave
		GROUP BY n.IdEtiqueta;

		COMMIT TRANSACTION;

		SELECT COUNT(*) AS Registradas FROM @nuevas;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO

-- ---------------------------------------------------------
-- TRADUCCION
-- ---------------------------------------------------------

-- Textos a mostrar en un idioma: si una etiqueta no esta traducida se usa el texto del idioma por defecto
CREATE OR ALTER PROCEDURE usp_Traduccion_GetByIdioma
	@CodigoIdioma VARCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT e.Clave, COALESCE(t.Texto, d.Texto) AS Texto
	FROM dbo.Etiqueta e
	LEFT JOIN dbo.Traduccion t ON t.IdEtiqueta = e.IdEtiqueta AND t.CodigoIdioma = @CodigoIdioma
	LEFT JOIN dbo.Traduccion d ON d.IdEtiqueta = e.IdEtiqueta
		AND d.CodigoIdioma = (SELECT Codigo FROM dbo.Idioma WHERE EsDefault = 1)
	WHERE COALESCE(t.Texto, d.Texto) IS NOT NULL;
END
GO

-- Vista de edicion: cada etiqueta con su texto de referencia (idioma por defecto) y su
-- traduccion en el idioma pedido (NULL si todavia no fue traducida)
CREATE OR ALTER PROCEDURE usp_Traduccion_GetMatriz
	@CodigoIdioma VARCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		e.Clave,
		e.Formulario,
		d.Texto AS TextoReferencia,
		t.Texto AS TextoTraducido
	FROM dbo.Etiqueta e
	LEFT JOIN dbo.Traduccion t ON t.IdEtiqueta = e.IdEtiqueta AND t.CodigoIdioma = @CodigoIdioma
	LEFT JOIN dbo.Traduccion d ON d.IdEtiqueta = e.IdEtiqueta
		AND d.CodigoIdioma = (SELECT Codigo FROM dbo.Idioma WHERE EsDefault = 1)
	ORDER BY CASE WHEN e.Formulario IS NULL THEN 1 ELSE 0 END, e.Formulario, e.Clave;
END
GO

-- Guarda un lote de traducciones de un idioma.
-- Texto con contenido: inserta o actualiza. Texto vacio/NULL: elimina la traduccion (vuelve a
-- mostrarse el texto del idioma por defecto), salvo en el idioma por defecto, donde se ignora.
CREATE OR ALTER PROCEDURE usp_Traduccion_GuardarLote
	@CodigoIdioma VARCHAR(5),
	@Traducciones dbo.TraduccionTablaTipo READONLY
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.Idioma WHERE Codigo = @CodigoIdioma)
		THROW 50003, 'El idioma indicado no existe.', 1;

	DECLARE @esDefault BIT = (SELECT EsDefault FROM dbo.Idioma WHERE Codigo = @CodigoIdioma);

	BEGIN TRY
		BEGIN TRANSACTION;

		UPDATE t
		SET t.Texto = src.Texto
		FROM dbo.Traduccion t
		INNER JOIN dbo.Etiqueta e ON e.IdEtiqueta = t.IdEtiqueta
		INNER JOIN @Traducciones src ON src.Clave = e.Clave
		WHERE t.CodigoIdioma = @CodigoIdioma
		  AND LEN(LTRIM(RTRIM(ISNULL(src.Texto, N'')))) > 0;

		INSERT INTO dbo.Traduccion (IdEtiqueta, CodigoIdioma, Texto)
		SELECT e.IdEtiqueta, @CodigoIdioma, src.Texto
		FROM @Traducciones src
		INNER JOIN dbo.Etiqueta e ON e.Clave = src.Clave
		WHERE LEN(LTRIM(RTRIM(ISNULL(src.Texto, N'')))) > 0
		  AND NOT EXISTS (SELECT 1 FROM dbo.Traduccion t WHERE t.IdEtiqueta = e.IdEtiqueta AND t.CodigoIdioma = @CodigoIdioma);

		IF @esDefault = 0
		BEGIN
			DELETE t
			FROM dbo.Traduccion t
			INNER JOIN dbo.Etiqueta e ON e.IdEtiqueta = t.IdEtiqueta
			INNER JOIN @Traducciones src ON src.Clave = e.Clave
			WHERE t.CodigoIdioma = @CodigoIdioma
			  AND LEN(LTRIM(RTRIM(ISNULL(src.Texto, N'')))) = 0;
		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
