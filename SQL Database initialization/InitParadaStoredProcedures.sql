CREATE PROCEDURE usp_Parada_GetAll
AS
BEGIN
	SELECT * FROM Parada;
END
GO

CREATE PROCEDURE usp_Parada_Insert
	@id VARCHAR(20),
	@descripcion VARCHAR(255),
	@localidad VARCHAR(100),
	@direccion VARCHAR(200),
	@habilitada BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Parada WHERE id = @id)
		RETURN;

	INSERT INTO Parada VALUES (
		@id,
		@descripcion,
		@localidad,
		@direccion,
		@habilitada
	);
END
GO

CREATE PROCEDURE usp_Parada_Delete
	@id VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Parada WHERE id = @id)
		RETURN;

	DELETE FROM Parada WHERE id = @id;
END
GO

CREATE PROCEDURE usp_Parada_Update
	@id VARCHAR(20),
	@descripcion VARCHAR(255),
	@localidad VARCHAR(100),
	@direccion VARCHAR(200),
	@habilitada BIT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Parada WHERE id = @id)
		RETURN;

	UPDATE Parada SET
		descripcion = @descripcion,
		localidad = @localidad,
		direccion = @direccion,
		habilitada = @habilitada
	WHERE id = @id
END