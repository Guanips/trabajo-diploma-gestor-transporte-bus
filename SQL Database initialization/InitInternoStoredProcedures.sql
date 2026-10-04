USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: Interno (Alta, Baja y Modificacion)
-- =========================================================

-- Interno ya no tiene num_chofer: se elimina el procedimiento de asignacion de chofer
CREATE OR ALTER PROCEDURE usp_Interno_GetAll
AS
BEGIN
	SELECT * FROM Interno;
END
GO

CREATE OR ALTER PROCEDURE usp_Interno_Insert
	@patente VARCHAR(20),
	@modelo VARCHAR(50),
	@fechaIncorporacion DATE,
	@disponible BIT
AS
BEGIN
	-- No se permite un interno con una patente ya existente
	IF EXISTS (SELECT 1 FROM Interno WHERE patente = @patente)
		RETURN;

	INSERT INTO Interno (patente, modelo, fechaIncorporacion, disponible)
	VALUES (@patente, @modelo, @fechaIncorporacion, @disponible);

	-- Devuelve el num_interno generado (IDENTITY) para la capa superior
	SELECT CAST(SCOPE_IDENTITY() AS INT) AS num_interno;
END
GO

CREATE OR ALTER PROCEDURE usp_Interno_Update
	@num_interno INT,
	@patente VARCHAR(20),
	@modelo VARCHAR(50),
	@fechaIncorporacion DATE,
	@disponible BIT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
		RETURN;

	-- No se permite cambiar la patente a una ya asignada a otro interno
	IF EXISTS (SELECT 1 FROM Interno WHERE patente = @patente AND num_interno <> @num_interno)
		RETURN;

	UPDATE Interno SET
		patente = @patente,
		modelo = @modelo,
		fechaIncorporacion = @fechaIncorporacion,
		disponible = @disponible
	WHERE num_interno = @num_interno;
END
GO

CREATE OR ALTER PROCEDURE usp_Interno_Delete
	@num_interno INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Interno WHERE num_interno = @num_interno)
		RETURN;

	DELETE FROM Interno WHERE num_interno = @num_interno;
END
GO
