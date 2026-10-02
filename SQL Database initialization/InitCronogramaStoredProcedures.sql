-- =========================================================
-- Stored Procedures: Cronograma (Alta, Baja y Modificacion)
-- =========================================================

-- Permite re-ejecutar este script sobre una base que ya tiene los procedimientos creados
IF OBJECT_ID(N'usp_Cronograma_GetAll', N'P') IS NOT NULL DROP PROCEDURE usp_Cronograma_GetAll;
GO
IF OBJECT_ID(N'usp_Cronograma_Insert', N'P') IS NOT NULL DROP PROCEDURE usp_Cronograma_Insert;
GO
IF OBJECT_ID(N'usp_Cronograma_Update', N'P') IS NOT NULL DROP PROCEDURE usp_Cronograma_Update;
GO
IF OBJECT_ID(N'usp_Cronograma_Delete', N'P') IS NOT NULL DROP PROCEDURE usp_Cronograma_Delete;
GO

CREATE PROCEDURE usp_Cronograma_GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Cronograma;
END
GO

CREATE PROCEDURE usp_Cronograma_Insert
	@descripcion NVARCHAR(MAX),
	@fechaValidez DATE,
	@horaInicio TIME,
	@horaFin TIME,
	@frecuenciaMinutos INT,
	@tiempoDescansoMinutos INT,
	@id_ruta VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Se valida que la ruta exista antes de insertar el cronograma
		IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id = @id_ruta)
			RETURN;

		INSERT INTO Cronograma (
			descripcion,
			fechaValidez,
			horaInicio,
			horaFin,
			frecuenciaMinutos,
			tiempoDescansoMinutos,
			id_ruta
		)
		VALUES (
			@descripcion,
			@fechaValidez,
			@horaInicio,
			@horaFin,
			@frecuenciaMinutos,
			@tiempoDescansoMinutos,
			@id_ruta
		);

		-- Devuelve el id generado (IDENTITY) para la capa superior
		SELECT CAST(SCOPE_IDENTITY() AS INT) AS id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE PROCEDURE usp_Cronograma_Update
	@id INT,
	@descripcion NVARCHAR(MAX),
	@fechaValidez DATE,
	@horaInicio TIME,
	@horaFin TIME,
	@frecuenciaMinutos INT,
	@tiempoDescansoMinutos INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Cronograma WHERE id = @id)
			RETURN;

		UPDATE Cronograma SET
			descripcion = @descripcion,
			fechaValidez = @fechaValidez,
			horaInicio = @horaInicio,
			horaFin = @horaFin,
			frecuenciaMinutos = @frecuenciaMinutos,
			tiempoDescansoMinutos = @tiempoDescansoMinutos
		WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE PROCEDURE usp_Cronograma_Delete
	@id INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Cronograma WHERE id = @id)
			RETURN;

		DELETE FROM Cronograma WHERE id = @id;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO