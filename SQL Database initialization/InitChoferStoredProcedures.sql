USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: Chofer (Alta, Baja y Modificacion)
-- =========================================================

CREATE OR ALTER PROCEDURE usp_Chofer_GetAll
AS
BEGIN
	SELECT * FROM Chofer;
END
GO

CREATE OR ALTER PROCEDURE usp_Chofer_Insert
	@dni INT,
	@nombreCompleto VARCHAR(100),
	@activo BIT = 1
AS
BEGIN
	-- No se permite un chofer con un DNI ya existente
	IF EXISTS (SELECT 1 FROM Chofer WHERE dni = @dni)
		RETURN;

	INSERT INTO Chofer (dni, nombreCompleto, activo)
	VALUES (@dni, @nombreCompleto, @activo);

	-- Devuelve el num_chofer generado (IDENTITY) para la capa superior
	SELECT CAST(SCOPE_IDENTITY() AS INT) AS num_chofer;
END
GO

CREATE OR ALTER PROCEDURE usp_Chofer_Update
	@num_chofer INT,
	@dni INT,
	@nombreCompleto VARCHAR(100),
	@activo BIT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Chofer WHERE num_chofer = @num_chofer)
		RETURN;

	-- No se permite cambiar el DNI a uno ya asignado a otro chofer
	IF EXISTS (SELECT 1 FROM Chofer WHERE dni = @dni AND num_chofer <> @num_chofer)
		RETURN;

	UPDATE Chofer SET
		dni = @dni,
		nombreCompleto = @nombreCompleto,
		activo = @activo
	WHERE num_chofer = @num_chofer;
END
GO

CREATE OR ALTER PROCEDURE usp_Chofer_Delete
	@num_chofer INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Chofer WHERE num_chofer = @num_chofer)
		RETURN;

	DELETE FROM Chofer WHERE num_chofer = @num_chofer;
END
GO
