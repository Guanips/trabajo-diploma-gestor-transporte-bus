USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: RevisionTaller, OrdenReparacion, DetalleOrdenReparacion
-- =========================================================

-- =========================================================
-- RevisionTaller Stored Procedures
-- =========================================================

CREATE OR ALTER PROCEDURE usp_RevisionTaller_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM RevisionTaller;
END
GO

CREATE OR ALTER PROCEDURE usp_RevisionTaller_Insert
	@num_interno INT,
	@fecha DATE,
	@descripcion NVARCHAR(MAX),
	@reparacionRequerida BIT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que el interno exista antes de insertar la revision
		IF NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
			RETURN;

		-- Valida que la descripcion no sea vacia
		IF @descripcion IS NULL OR LEN(@descripcion) = 0
			RETURN;

		INSERT INTO RevisionTaller (
			num_interno,
			fecha,
			descripcion,
			reparacionRequerida
		)
		VALUES (
			@num_interno,
			@fecha,
			@descripcion,
			@reparacionRequerida
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

-- =========================================================
-- OrdenReparacion Stored Procedures
-- =========================================================

CREATE OR ALTER PROCEDURE usp_OrdenReparacion_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM OrdenReparacion;
END
GO

CREATE OR ALTER PROCEDURE usp_OrdenReparacion_Insert
	@id_revision INT,
	@motivoReparacion NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que la revision exista antes de insertar la orden
		IF NOT EXISTS (SELECT 1 FROM RevisionTaller WHERE id = @id_revision)
			RETURN;

		-- Valida que el motivo no sea vacio
		IF @motivoReparacion IS NULL OR LEN(@motivoReparacion) = 0
			RETURN;

		INSERT INTO OrdenReparacion (
			id_revision,
			motivoReparacion
		)
		VALUES (
			@id_revision,
			@motivoReparacion
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

-- =========================================================
-- DetalleOrdenReparacion Stored Procedures
-- =========================================================

CREATE OR ALTER PROCEDURE usp_DetalleOrdenReparacion_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM DetalleOrdenReparacion;
END
GO

CREATE OR ALTER PROCEDURE usp_DetalleOrdenReparacion_Insert
	@id_ordenReparacion INT,
	@insumo NVARCHAR(MAX),
	@cantidad INT,
	@costoUnitario INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que la orden de reparacion exista
		IF NOT EXISTS (SELECT 1 FROM OrdenReparacion WHERE id = @id_ordenReparacion)
			RETURN;

		-- Valida que el insumo no sea vacio
		IF @insumo IS NULL OR LEN(@insumo) = 0
			RETURN;

		-- Valida que la cantidad sea mayor a cero
		IF @cantidad <= 0
			RETURN;

		-- Valida que el costoUnitario no sea negativo (si se proporciona)
		IF @costoUnitario IS NOT NULL AND @costoUnitario < 0
			RETURN;

		INSERT INTO DetalleOrdenReparacion (
			id_ordenReparacion,
			insumo,
			cantidad,
			costoUnitario
		)
		VALUES (
			@id_ordenReparacion,
			@insumo,
			@cantidad,
			@costoUnitario
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_DetalleOrdenReparacion_UpdateCostoUnitario
	@id INT,
	@costoUnitario INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que el detalle exista
		IF NOT EXISTS (SELECT 1 FROM DetalleOrdenReparacion WHERE id = @id)
			RETURN;

		-- Valida que el costoUnitario no sea negativo
		IF @costoUnitario < 0
			RETURN;

		UPDATE DetalleOrdenReparacion
		SET costoUnitario = @costoUnitario
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO
