USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: CargaCombustible (Obtener, Insertar, Anular)
-- =========================================================

CREATE OR ALTER PROCEDURE usp_CargaCombustible_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CargaCombustible;
END
GO

CREATE OR ALTER PROCEDURE usp_CargaCombustible_Insert
	@num_interno INT,
	@fechaHora DATETIME,
	@litrosCargados DECIMAL(10,2),
	@precioPorLitro DECIMAL(10,2),
	@kilometrajeActual INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que el interno exista antes de insertar la carga
		IF NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
			RETURN;

		-- Valida que litrosCargados sea mayor a cero
		IF @litrosCargados <= 0
			RETURN;

		-- Valida que precioPorLitro no sea negativo
		IF @precioPorLitro < 0
			RETURN;

		-- Valida que kilometrajeActual no sea negativo
		IF @kilometrajeActual < 0
			RETURN;

		INSERT INTO CargaCombustible (
			num_interno,
			fechaHora,
			litrosCargados,
			precioPorLitro,
			kilometrajeActual,
			anulada
		)
		VALUES (
			@num_interno,
			@fechaHora,
			@litrosCargados,
			@precioPorLitro,
			@kilometrajeActual,
			0
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_CargaCombustible_Anular
	@id INT,
	@motivoAnulacion NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM CargaCombustible WHERE id = @id)
			RETURN;

		-- Valida que no esté ya anulada
		IF EXISTS (SELECT 1 FROM CargaCombustible WHERE id = @id AND anulada = 1)
			RETURN;

		UPDATE CargaCombustible
		SET anulada = 1,
			motivoAnulacion = @motivoAnulacion
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO
