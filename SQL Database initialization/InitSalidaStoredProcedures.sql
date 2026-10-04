USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: Salida (Alta, Baja y Asignaciones)
-- =========================================================

CREATE OR ALTER PROCEDURE usp_Salida_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Salida;
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_Insert
	@id_cronograma INT,
	@num_interno INT = NULL,
	@num_chofer INT = NULL,
	@horaSalidaTeorica TIME,
	@horaLlegadaTeorica TIME,
	@horaSalidaReal TIME = NULL,
	@horaLlegadaReal TIME = NULL,
	@estaSuspendida BIT,
	@motivoSuspension NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que el cronograma exista antes de insertar la salida
		IF NOT EXISTS (SELECT 1 FROM Cronograma WHERE id = @id_cronograma)
			RETURN;

		-- Si se informa un interno, debe existir
		IF @num_interno IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
			RETURN;

		-- Si se informa un chofer, debe existir
		IF @num_chofer IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Chofer WHERE num_chofer = @num_chofer)
			RETURN;

		INSERT INTO Salida (
			id_cronograma,
			num_interno,
			num_chofer,
			horaSalidaTeorica,
			horaLlegadaTeorica,
			horaSalidaReal,
			horaLlegadaReal,
			estaSuspendida,
			motivoSuspension
		)
		VALUES (
			@id_cronograma,
			@num_interno,
			@num_chofer,
			@horaSalidaTeorica,
			@horaLlegadaTeorica,
			@horaSalidaReal,
			@horaLlegadaReal,
			@estaSuspendida,
			@motivoSuspension
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_AssignInterno
	@id INT,	
	@num_interno INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Salida WHERE id = @id)
			RETURN;

		-- Permite desasignar el interno enviando NULL
		IF @num_interno IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
			RETURN;

		UPDATE Salida
		SET num_interno = @num_interno
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_AssignChofer
	@id INT,
	@num_chofer INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Salida WHERE id = @id)
			RETURN;

		-- Permite desasignar el chofer enviando NULL
		IF @num_chofer IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Chofer WHERE num_chofer = @num_chofer)
			RETURN;

		UPDATE Salida
		SET num_chofer = @num_chofer
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_Suspender
	@id INT,
	@motivoSuspension NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Salida WHERE id = @id)
			RETURN;

		UPDATE Salida
		SET estaSuspendida = 1,
			motivoSuspension = @motivoSuspension
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE usp_Salida_Delete
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Salida WHERE id = @id)
			RETURN;

		DELETE FROM Salida WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO
