USE GestorTransporteCG;
GO

-- =========================================================
-- Stored Procedures: Reporte general del servicio (analisis de informacion)
-- =========================================================

-- Devuelve, en un solo viaje, todos los datos del reporte para el rango [@desde, @hasta] (fechas inclusive).
-- Las salidas no tienen fecha propia: se usa la fechaValidez de su cronograma.
-- Result sets, en este orden (el BLL los lee por posicion):
--   1. Resumen general (una fila)
--   2. Rendimiento por ruta
--   3. Evolucion diaria de la puntualidad
--   4. Distribucion de retrasos por tramo (siempre 5 filas)
--   5. Suspensiones por motivo
--   6. Choferes con mas sanciones (top 10)
--   7. Combustible por mes
--   8. Taller por mes
--   9. Taller por interno (top 5 por gasto)
--  10. Consumo de combustible por interno
-- Criterios: retraso = minutos entre la llegada teorica y la real (solo salidas no suspendidas con llegada registrada);
-- puntual = retraso <= @toleranciaMin; las cargas anuladas no se cuentan; los insumos sin costo no suman al gasto.
CREATE OR ALTER PROCEDURE usp_Reporte_General
	@desde DATE,
	@hasta DATE,
	@toleranciaMin INT = 5
AS
BEGIN
	SET NOCOUNT ON;

	SELECT s.id,
	       c.fechaValidez AS fecha,
	       c.id_ruta,
	       s.num_chofer,
	       s.estaSuspendida,
	       s.motivoSuspension,
	       CASE WHEN s.estaSuspendida = 0 AND s.horaLlegadaReal IS NOT NULL
	            THEN DATEDIFF(MINUTE, s.horaLlegadaTeorica, s.horaLlegadaReal) END AS retraso
	INTO #S
	FROM Salida s
	JOIN Cronograma c ON c.id = s.id_cronograma
	WHERE c.fechaValidez BETWEEN @desde AND @hasta;

	-- 1. Resumen general
	SELECT
		(SELECT COUNT(*) FROM #S) AS salidasProgramadas,
		(SELECT COUNT(*) FROM #S WHERE estaSuspendida = 1) AS salidasSuspendidas,
		(SELECT COUNT(retraso) FROM #S) AS salidasConLlegada,
		(SELECT COUNT(*) FROM #S WHERE retraso <= @toleranciaMin) AS salidasPuntuales,
		ISNULL((SELECT AVG(CASE WHEN retraso > 0 THEN retraso * 1.0 ELSE 0 END) FROM #S WHERE retraso IS NOT NULL), 0) AS retrasoPromedioMin,
		(SELECT COUNT(*) FROM Sancion WHERE fecha BETWEEN @desde AND @hasta) AS sanciones,
		ISNULL((SELECT SUM(litrosCargados) FROM CargaCombustible
		        WHERE anulada = 0 AND CAST(fechaHora AS DATE) BETWEEN @desde AND @hasta), 0) AS litrosCombustible,
		ISNULL((SELECT SUM(CAST(litrosCargados AS DECIMAL(18, 2)) * precioPorLitro) FROM CargaCombustible
		        WHERE anulada = 0 AND CAST(fechaHora AS DATE) BETWEEN @desde AND @hasta), 0) AS gastoCombustible,
		(SELECT COUNT(*) FROM RevisionTaller WHERE fecha BETWEEN @desde AND @hasta) AS revisiones,
		(SELECT COUNT(*) FROM RevisionTaller WHERE fecha BETWEEN @desde AND @hasta AND reparacionRequerida = 1) AS reparaciones,
		ISNULL((SELECT SUM(CAST(d.cantidad AS DECIMAL(18, 2)) * d.costoUnitario)
		        FROM RevisionTaller r
		        JOIN OrdenReparacion o ON o.id_revision = r.id
		        JOIN DetalleOrdenReparacion d ON d.id_ordenReparacion = o.id
		        WHERE r.fecha BETWEEN @desde AND @hasta), 0) AS gastoTaller,
		(SELECT COUNT(*)
		 FROM RevisionTaller r
		 JOIN OrdenReparacion o ON o.id_revision = r.id
		 JOIN DetalleOrdenReparacion d ON d.id_ordenReparacion = o.id
		 WHERE r.fecha BETWEEN @desde AND @hasta AND d.costoUnitario IS NULL) AS insumosSinCosto;

	-- 2. Rendimiento por ruta
	SELECT r.id, r.descripcion, r.sentido,
	       COUNT(*) AS programadas,
	       SUM(CASE WHEN x.estaSuspendida = 1 THEN 1 ELSE 0 END) AS suspendidas,
	       COUNT(x.retraso) AS conLlegada,
	       SUM(CASE WHEN x.retraso <= @toleranciaMin THEN 1 ELSE 0 END) AS puntuales,
	       ISNULL(AVG(CASE WHEN x.retraso IS NULL THEN NULL WHEN x.retraso > 0 THEN x.retraso * 1.0 ELSE 0 END), 0) AS retrasoPromedioMin
	FROM #S x
	JOIN Ruta r ON r.id = x.id_ruta
	GROUP BY r.id, r.descripcion, r.sentido
	ORDER BY r.id;

	-- 3. Evolucion diaria de la puntualidad
	SELECT fecha,
	       COUNT(retraso) AS conLlegada,
	       SUM(CASE WHEN retraso <= @toleranciaMin THEN 1 ELSE 0 END) AS puntuales,
	       AVG(CASE WHEN retraso > 0 THEN retraso * 1.0 ELSE 0 END) AS retrasoPromedioMin
	FROM #S
	WHERE retraso IS NOT NULL
	GROUP BY fecha
	ORDER BY fecha;

	-- 4. Distribucion de retrasos: 1 puntual, 2 leve (hasta 10), 3 moderado (hasta 25), 4 grave (hasta 50), 5 muy grave
	SELECT t.tramo, COUNT(x.retraso) AS cantidad
	FROM (VALUES (1), (2), (3), (4), (5)) AS t(tramo)
	LEFT JOIN (
		SELECT retraso,
		       CASE WHEN retraso <= @toleranciaMin THEN 1
		            WHEN retraso <= 10 THEN 2
		            WHEN retraso <= 25 THEN 3
		            WHEN retraso <= 50 THEN 4
		            ELSE 5 END AS tramo
		FROM #S WHERE retraso IS NOT NULL
	) x ON x.tramo = t.tramo
	GROUP BY t.tramo
	ORDER BY t.tramo;

	-- 5. Suspensiones por motivo
	SELECT ISNULL(NULLIF(LTRIM(RTRIM(CAST(motivoSuspension AS NVARCHAR(200)))), N''), N'Sin motivo') AS motivo,
	       COUNT(*) AS cantidad
	FROM #S
	WHERE estaSuspendida = 1
	GROUP BY ISNULL(NULLIF(LTRIM(RTRIM(CAST(motivoSuspension AS NVARCHAR(200)))), N''), N'Sin motivo')
	ORDER BY COUNT(*) DESC;

	-- 6. Choferes con mas sanciones
	SELECT TOP (10)
	       ch.nombreCompleto,
	       COUNT(*) AS sanciones,
	       ISNULL((SELECT AVG(CASE WHEN retraso > 0 THEN retraso * 1.0 ELSE 0 END)
	               FROM #S WHERE num_chofer = ch.num_chofer AND retraso IS NOT NULL), 0) AS retrasoPromedioMin
	FROM Sancion sa
	JOIN Chofer ch ON ch.num_chofer = sa.num_chofer
	WHERE sa.fecha BETWEEN @desde AND @hasta
	GROUP BY ch.num_chofer, ch.nombreCompleto
	ORDER BY COUNT(*) DESC, ch.nombreCompleto;

	-- 7. Combustible por mes
	SELECT CONVERT(CHAR(7), fechaHora, 126) AS mes,
	       SUM(litrosCargados) AS litros,
	       SUM(CAST(litrosCargados AS DECIMAL(18, 2)) * precioPorLitro) AS gasto
	FROM CargaCombustible
	WHERE anulada = 0 AND CAST(fechaHora AS DATE) BETWEEN @desde AND @hasta
	GROUP BY CONVERT(CHAR(7), fechaHora, 126)
	ORDER BY mes;

	-- 8. Taller por mes
	SELECT CONVERT(CHAR(7), r.fecha, 126) AS mes,
	       COUNT(DISTINCT r.id) AS revisiones,
	       ISNULL(SUM(CAST(d.cantidad AS DECIMAL(18, 2)) * d.costoUnitario), 0) AS gasto
	FROM RevisionTaller r
	LEFT JOIN OrdenReparacion o ON o.id_revision = r.id
	LEFT JOIN DetalleOrdenReparacion d ON d.id_ordenReparacion = o.id
	WHERE r.fecha BETWEEN @desde AND @hasta
	GROUP BY CONVERT(CHAR(7), r.fecha, 126)
	ORDER BY mes;

	-- 9. Taller por interno (top 5 por gasto)
	SELECT TOP (5)
	       i.patente, i.modelo,
	       COUNT(DISTINCT r.id) AS revisiones,
	       COUNT(DISTINCT CASE WHEN r.reparacionRequerida = 1 THEN r.id END) AS reparaciones,
	       ISNULL(SUM(CAST(d.cantidad AS DECIMAL(18, 2)) * d.costoUnitario), 0) AS gasto
	FROM RevisionTaller r
	JOIN Interno i ON i.num_interno = r.num_interno
	LEFT JOIN OrdenReparacion o ON o.id_revision = r.id
	LEFT JOIN DetalleOrdenReparacion d ON d.id_ordenReparacion = o.id
	WHERE r.fecha BETWEEN @desde AND @hasta
	GROUP BY i.num_interno, i.patente, i.modelo
	ORDER BY ISNULL(SUM(CAST(d.cantidad AS DECIMAL(18, 2)) * d.costoUnitario), 0) DESC, i.patente;

	-- 10. Consumo por interno: litros de las cargas posteriores a la primera / km recorridos entre la primera y la ultima carga
	;WITH cargas AS (
		SELECT num_interno, kilometrajeActual AS km, litrosCargados AS litros,
		       ROW_NUMBER() OVER (PARTITION BY num_interno ORDER BY fechaHora) AS rn,
		       ROW_NUMBER() OVER (PARTITION BY num_interno ORDER BY fechaHora DESC) AS rnDesc
		FROM CargaCombustible
		WHERE anulada = 0 AND CAST(fechaHora AS DATE) BETWEEN @desde AND @hasta
	)
	SELECT i.patente, i.modelo,
	       SUM(CASE WHEN c.rn > 1 THEN c.litros ELSE 0 END) AS litros,
	       MAX(CASE WHEN c.rnDesc = 1 THEN c.km END) - MAX(CASE WHEN c.rn = 1 THEN c.km END) AS km
	FROM cargas c
	JOIN Interno i ON i.num_interno = c.num_interno
	GROUP BY i.num_interno, i.patente, i.modelo
	HAVING COUNT(*) >= 2
	ORDER BY i.patente;
END
GO
