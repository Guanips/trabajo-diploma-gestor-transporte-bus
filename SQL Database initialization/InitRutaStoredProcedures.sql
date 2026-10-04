USE GestorTransporteCG;
GO

CREATE OR ALTER PROCEDURE usp_Ruta_GetAll
AS
BEGIN
	SELECT * FROM Ruta;
END
GO

CREATE OR ALTER PROCEDURE usp_Recorrido_GetForRuta
	@id_ruta VARCHAR(20)
AS
BEGIN
	SELECT RR.id_parada, RR.orden FROM Recorrido_Ruta as RR INNER JOIN Parada as P ON RR.id_parada=P.id WHERE RR.id_ruta=@id_ruta
END
GO

CREATE OR ALTER PROCEDURE usp_Ruta_Insert
	@id VARCHAR(20),
	@descripcion VARCHAR(255),
	@sentido VARCHAR(20),
	@distanciaTotalKM INT,
	@tiempoEstimadoMin INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Ruta WHERE id = @id)
		RETURN;
	INSERT INTO Ruta VALUES (@id, @descripcion, @sentido, @distanciaTotalKM, @tiempoEstimadoMin);
END
GO

CREATE OR ALTER PROCEDURE usp_Ruta_Delete
	@id VARCHAR(20)
AS
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id=@id)
		RETURN
	DELETE FROM Ruta WHERE id = @id;
END
GO

CREATE OR ALTER PROCEDURE usp_Ruta_Update
	@id VARCHAR(20),
	@descripcion VARCHAR(255),
	@sentido VARCHAR(20),
	@distanciaTotalKM INT,
	@tiempoEstimadoMin INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id=@id)
		RETURN

	UPDATE Ruta SET
		descripcion = @descripcion,
		sentido = @sentido,
		distanciaTotalKM = @distanciaTotalKM,
		tiempoEstimadoMin = @tiempoEstimadoMin
	WHERE id = @id
END
GO

CREATE OR ALTER PROCEDURE usp_Recorrido_AddParada
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20),
	@orden INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id=@id_ruta)
		RETURN
	IF NOT EXISTS (SELECT 1 FROM Parada WHERE id=@id_parada)
		RETURN

	INSERT INTO Recorrido_Ruta VALUES (@id_ruta, @id_parada, @orden);
END
GO

CREATE OR ALTER PROCEDURE usp_Recorrido_RemoveParada
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada)

	DELETE FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada;
END
GO

CREATE OR ALTER PROCEDURE usp_Recorrido_EditParadaOrder
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20),
	@orden INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada)

	UPDATE Recorrido_Ruta SET
		orden=@orden
	WHERE id_ruta=@id_ruta AND id_parada=@id_parada;
END
GO