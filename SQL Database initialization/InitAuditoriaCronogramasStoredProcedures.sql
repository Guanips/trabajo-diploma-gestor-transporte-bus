USE GestorTransporteCG;
GO

-- =========================================================
-- Sancion Stored Procedures
-- =========================================================

CREATE OR ALTER PROCEDURE usp_Sancion_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Sancion;
END
GO

CREATE OR ALTER PROCEDURE usp_Sancion_GetByChofer
	@num_chofer INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Sancion
	WHERE num_chofer = @num_chofer
	ORDER BY fecha DESC;
END
GO

CREATE OR ALTER PROCEDURE usp_Sancion_Insert
	@num_chofer INT,
	@fecha DATE,
	@motivo NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que el chofer exista antes de insertar la sancion
		IF NOT EXISTS (SELECT 1 FROM Chofer WHERE num_chofer = @num_chofer)
			RETURN;

		-- Valida que el motivo no sea vacio
		IF @motivo IS NULL OR LEN(@motivo) = 0
			RETURN;

		INSERT INTO Sancion (
			num_chofer,
			fecha,
			motivo
		)
		VALUES (
			@num_chofer,
			@fecha,
			@motivo
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Sancion_Delete
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que la sancion exista
		IF NOT EXISTS (SELECT 1 FROM Sancion WHERE id = @id)
			RETURN;

		DELETE FROM Sancion
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

-- =========================================================
-- Salida Stored Procedures (Auditoria de Cronogramas)
-- =========================================================

CREATE OR ALTER PROCEDURE usp_Salida_UpdateHoraLlegadaReal
	@id INT,
	@horaLlegadaReal TIME
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que la salida exista
		IF NOT EXISTS (SELECT 1 FROM Salida WHERE id = @id)
			RETURN;

		-- Valida que la hora no sea nula
		IF @horaLlegadaReal IS NULL
			RETURN;

		UPDATE Salida
		SET horaLlegadaReal = @horaLlegadaReal
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_GetById
	@id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Salida
	WHERE id = @id;
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_GetByCronograma
	@id_cronograma INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Salida
	WHERE id_cronograma = @id_cronograma
	ORDER BY horaSalidaTeorica ASC;
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_GetByChofer
	@num_chofer INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Salida
	WHERE num_chofer = @num_chofer
	ORDER BY id DESC;
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_GetByInterno
	@num_interno INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Salida
	WHERE num_interno = @num_interno
	ORDER BY id DESC;
END
GO
