-- Permite re-ejecutar este script sobre una base que ya tiene los procedimientos creados
IF OBJECT_ID(N'usp_Ruta_GetAll', N'P') IS NOT NULL DROP PROCEDURE usp_Ruta_GetAll;
GO
IF OBJECT_ID(N'usp_Recorrido_GetForRuta', N'P') IS NOT NULL DROP PROCEDURE usp_Recorrido_GetForRuta;
GO
IF OBJECT_ID(N'usp_Ruta_Insert', N'P') IS NOT NULL DROP PROCEDURE usp_Ruta_Insert;
GO
IF OBJECT_ID(N'usp_Ruta_Delete', N'P') IS NOT NULL DROP PROCEDURE usp_Ruta_Delete;
GO
IF OBJECT_ID(N'usp_Ruta_Update', N'P') IS NOT NULL DROP PROCEDURE usp_Ruta_Update;
GO
IF OBJECT_ID(N'usp_Recorrido_AddParada', N'P') IS NOT NULL DROP PROCEDURE usp_Recorrido_AddParada;
GO
IF OBJECT_ID(N'usp_Recorrido_RemoveParada', N'P') IS NOT NULL DROP PROCEDURE usp_Recorrido_RemoveParada;
GO
IF OBJECT_ID(N'usp_Recorrido_EditParadaOrder', N'P') IS NOT NULL DROP PROCEDURE usp_Recorrido_EditParadaOrder;
GO

CREATE PROCEDURE usp_Ruta_GetAll
AS
BEGIN
	SELECT * FROM Ruta;
END
GO

CREATE PROCEDURE usp_Recorrido_GetForRuta
	@id_ruta VARCHAR(20)
AS
BEGIN
	SELECT RR.id_parada, RR.orden
	FROM Recorrido_Ruta as RR INNER JOIN Parada as P ON RR.id_parada=P.id
	WHERE RR.id_ruta=@id_ruta
	ORDER BY RR.orden
END
GO

CREATE PROCEDURE usp_Ruta_Insert
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

CREATE PROCEDURE usp_Ruta_Delete
	@id VARCHAR(20)
AS
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id=@id)
		RETURN
	DELETE FROM Ruta WHERE id = @id;
END
GO

CREATE PROCEDURE usp_Ruta_Update
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

CREATE PROCEDURE usp_Recorrido_AddParada
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20),
	@orden INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Ruta WHERE id=@id_ruta)
		RETURN
	IF NOT EXISTS (SELECT 1 FROM Parada WHERE id=@id_parada)
		RETURN

	-- Si el orden pedido ya esta ocupado en la ruta, la parada se agrega al final
	IF EXISTS (SELECT 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND orden=@orden)
		SELECT @orden = ISNULL(MAX(orden), 0) + 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta;

	INSERT INTO Recorrido_Ruta (id_ruta, id_parada, orden) VALUES (@id_ruta, @id_parada, @orden);
END
GO

CREATE PROCEDURE usp_Recorrido_RemoveParada
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada)
		RETURN;

	DELETE FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada;
END
GO

CREATE PROCEDURE usp_Recorrido_EditParadaOrder
	@id_ruta VARCHAR(20),
	@id_parada VARCHAR(20),
	@orden INT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada)
		RETURN;

	DECLARE @ordenActual INT;
	SELECT @ordenActual = orden FROM Recorrido_Ruta WHERE id_ruta=@id_ruta AND id_parada=@id_parada;

	DECLARE @cantidadParadas INT;
	SELECT @cantidadParadas = COUNT(*) FROM Recorrido_Ruta WHERE id_ruta=@id_ruta;

	-- El orden que llega desde la capa BLL es 1-based
	IF @orden < 1 OR @orden > @cantidadParadas OR @orden = @ordenActual
		RETURN;

	BEGIN TRANSACTION;

	IF @orden < @ordenActual
	BEGIN
		-- La parada sube: las paradas intermedias corren un lugar hacia atras
		UPDATE Recorrido_Ruta SET orden = orden + 1
		WHERE id_ruta=@id_ruta AND orden >= @orden AND orden < @ordenActual;
	END
	ELSE
	BEGIN
		-- La parada baja: las paradas intermedias corren un lugar hacia adelante
		UPDATE Recorrido_Ruta SET orden = orden - 1
		WHERE id_ruta=@id_ruta AND orden > @ordenActual AND orden <= @orden;
	END

	UPDATE Recorrido_Ruta SET orden = @orden
	WHERE id_ruta=@id_ruta AND id_parada=@id_parada;

	COMMIT TRANSACTION;
END
GO