-- Requiere que InitDatabase.sql y InitStartingData.sql ya se hayan ejecutado.
-- DATOS DE DEMOSTRACION: paradas, rutas, choferes, internos, cronogramas con sus salidas (con retrasos reales),
-- sanciones, cargas de combustible y revisiones/ordenes de reparacion del taller, para mostrar el funcionamiento
-- del programa y probar analisis sobre una ventana de tiempo.
--
-- Las fechas son RELATIVAS al dia de la inicializacion, asi la ventana siempre termina "hoy":
--   cronogramas/salidas : desde hace 75 dias hasta 4 dias en el futuro (el pasado tiene horas reales; el futuro, sin asignar en parte)
--   combustible         : ultimos 75 dias
--   taller y sanciones  : ultimos 180 dias
-- Los datos son deterministas (sin NEWID/RAND): dos inicializaciones el mismo dia generan lo mismo.
--
-- Es seguro re-ejecutarlo: solo corre si NO hay datos operativos (todo el script es una transaccion: todo o nada).
-- Para no cargar datos de demostracion, quitar "InitDemoData.sql" de la lista Scripts de DAL/DatabaseInitializer.cs.
USE GestorTransporteCG;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF NOT EXISTS (SELECT 1 FROM dbo.Parada)
   AND NOT EXISTS (SELECT 1 FROM dbo.Ruta)
   AND NOT EXISTS (SELECT 1 FROM dbo.Chofer)
   AND NOT EXISTS (SELECT 1 FROM dbo.Interno)
   AND NOT EXISTS (SELECT 1 FROM dbo.Cronograma)
   AND NOT EXISTS (SELECT 1 FROM dbo.CargaCombustible)
   AND NOT EXISTS (SELECT 1 FROM dbo.RevisionTaller)
   AND NOT EXISTS (SELECT 1 FROM dbo.Sancion)
BEGIN
    BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    -- Numeros 1..400 para generar series
    SELECT TOP (400) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
    INTO #N
    FROM sys.all_objects a CROSS JOIN sys.all_objects b;

    -- =====================================================
    -- PARADAS
    -- =====================================================
    INSERT INTO dbo.Parada (id, descripcion, localidad, direccion, habilitada) VALUES
    ('PAR-001', N'Terminal Moreno',                N'Moreno',            N'Av. Libertador 1450', 1),
    ('PAR-002', N'Estación Paso del Rey',          N'Paso del Rey',      N'Av. Gaona 3200', 1),
    ('PAR-003', N'Merlo Centro',                   N'Merlo',             N'Av. del Libertador 650', 1),
    ('PAR-004', N'Estación Padua',                 N'Padua',             N'Calle 25 de Mayo 800', 1),
    ('PAR-005', N'Ituzaingó Centro',               N'Ituzaingó',         N'Av. Rivadavia 21500', 1),
    ('PAR-006', N'Estación Castelar',              N'Castelar',          N'Av. Rivadavia 19800', 1),
    ('PAR-007', N'Plaza de Morón',                 N'Morón',             N'Av. Rivadavia 17900', 1),
    ('PAR-008', N'Ramos Mejía Centro',             N'Ramos Mejía',       N'Av. de Mayo 120', 1),
    ('PAR-009', N'Liniers - Av. Rivadavia',        N'Liniers',           N'Av. Rivadavia 11700', 1),
    ('PAR-010', N'Plaza Once',                     N'Ciudad de Buenos Aires', N'Av. Pueyrredón 15', 1),
    ('PAR-011', N'Terminal La Plata',              N'La Plata',          N'Calle 42 y 3', 1),
    ('PAR-012', N'Villa Elisa',                    N'Villa Elisa',       N'Camino Centenario 5400', 1),
    ('PAR-013', N'Berazategui Centro',             N'Berazategui',       N'Av. 14 1250', 1),
    ('PAR-014', N'Estación Quilmes',               N'Quilmes',           N'Av. Hipólito Yrigoyen 600', 1),
    ('PAR-015', N'Bernal Centro',                  N'Bernal',            N'Av. San Martín 300', 1),
    ('PAR-016', N'Estación Avellaneda',            N'Avellaneda',        N'Av. Mitre 750', 1),
    ('PAR-017', N'Estación Constitución',          N'Ciudad de Buenos Aires', N'Av. Brasil 1100', 1),
    ('PAR-018', N'Terminal Pilar',                 N'Pilar',             N'Ruta 8 km 52', 1),
    ('PAR-019', N'Del Viso',                       N'Del Viso',          N'Ruta 8 km 47', 1),
    ('PAR-020', N'José C. Paz Centro',             N'José C. Paz',       N'Av. Croacia 3300', 1),
    ('PAR-021', N'San Miguel Centro',              N'San Miguel',        N'Av. Presidente Perón 1100', 1),
    ('PAR-022', N'Estación Bella Vista',           N'Bella Vista',       N'Av. Pte. Perón 2400', 1),
    ('PAR-023', N'Estación Caseros',               N'Caseros',           N'Av. Hipólito Yrigoyen 5100', 1),
    ('PAR-024', N'Parada provisoria por obras',    N'Moreno',            N'Av. Márquez 2100', 0),
    ('PAR-025', N'Estación Ciudadela (en desuso)', N'Ciudadela',         N'Av. Rivadavia 14200', 0);

    -- =====================================================
    -- RUTAS Y RECORRIDOS (cada linea tiene una ruta de ida y su vuelta)
    -- =====================================================
    INSERT INTO dbo.Ruta (id, descripcion, sentido, distanciaTotalKM, tiempoEstimadoMin) VALUES
    ('RUT-001', N'Moreno - Once',                  'Ida',    38, 75),
    ('RUT-002', N'Once - Moreno',                  'Vuelta', 38, 80),
    ('RUT-003', N'La Plata - Constitución',        'Ida',    56, 80),
    ('RUT-004', N'Constitución - La Plata',        'Vuelta', 56, 85),
    ('RUT-005', N'Pilar - Liniers',                'Ida',    49, 85),
    ('RUT-006', N'Liniers - Pilar',                'Vuelta', 49, 90);

    INSERT INTO dbo.Recorrido_Ruta (id_ruta, id_parada, orden) VALUES
    ('RUT-001', 'PAR-001', 1), ('RUT-001', 'PAR-002', 2), ('RUT-001', 'PAR-003', 3), ('RUT-001', 'PAR-004', 4),
    ('RUT-001', 'PAR-005', 5), ('RUT-001', 'PAR-006', 6), ('RUT-001', 'PAR-007', 7), ('RUT-001', 'PAR-008', 8),
    ('RUT-001', 'PAR-009', 9), ('RUT-001', 'PAR-010', 10),
    ('RUT-003', 'PAR-011', 1), ('RUT-003', 'PAR-012', 2), ('RUT-003', 'PAR-013', 3), ('RUT-003', 'PAR-014', 4),
    ('RUT-003', 'PAR-015', 5), ('RUT-003', 'PAR-016', 6), ('RUT-003', 'PAR-017', 7),
    ('RUT-005', 'PAR-018', 1), ('RUT-005', 'PAR-019', 2), ('RUT-005', 'PAR-020', 3), ('RUT-005', 'PAR-021', 4),
    ('RUT-005', 'PAR-022', 5), ('RUT-005', 'PAR-023', 6), ('RUT-005', 'PAR-009', 7);

    -- La vuelta recorre las mismas paradas en orden inverso
    INSERT INTO dbo.Recorrido_Ruta (id_ruta, id_parada, orden)
    SELECT v.vuelta, r.id_parada, (SELECT MAX(orden) FROM dbo.Recorrido_Ruta WHERE id_ruta = r.id_ruta) + 1 - r.orden
    FROM dbo.Recorrido_Ruta r
    JOIN (VALUES ('RUT-001', 'RUT-002'), ('RUT-003', 'RUT-004'), ('RUT-005', 'RUT-006')) AS v(ida, vuelta) ON v.ida = r.id_ruta;

    -- =====================================================
    -- CHOFERES (1-18 titulares: 6 por linea; 19 suplente; 20 dado de baja)
    -- =====================================================
    INSERT INTO dbo.Chofer (dni, nombreCompleto, activo) VALUES
    (24531876, 'Juan Carlos Pereyra', 1),
    (27984512, 'Marcelo Daniel Gómez', 1),
    (30127845, 'Roberto Luis Fernández', 1),
    (22764309, 'Héctor Omar Sosa', 1),
    (29338761, 'Diego Alejandro Ramírez', 1),
    (31456220, 'Pablo Sebastián Acosta', 1),
    (26871934, 'Carlos Alberto Benítez', 1),
    (28345671, 'Gustavo Adrián Medina', 1),
    (33210987, 'Fabián Ezequiel Romero', 1),
    (25678123, 'Oscar Rubén Villalba', 1),
    (34012568, 'Leandro Nicolás Giménez', 1),
    (23459870, 'Raúl Ernesto Ortiz', 1),
    (29871456, 'Sergio Martín Castro', 1),
    (32765041, 'Mariano Gabriel Ríos', 1),
    (27123498, 'Walter Hugo Domínguez', 1),
    (30984715, 'Claudio Andrés Herrera', 1),
    (35120964, 'Matías Ignacio Luna', 1),
    (24893017, 'Julio César Molina', 1),
    (31872540, 'Ricardo Fabián Torres', 1),
    (21654398, 'Antonio Rubén Aguirre', 0);

    -- =====================================================
    -- INTERNOS (1-12 titulares: 4 por linea; 13 suplente; 8 y 14 en taller, no disponibles)
    -- =====================================================
    INSERT INTO dbo.Interno (patente, modelo, fechaIncorporacion, disponible) VALUES
    ('AE412KP', 'Mercedes-Benz OF 1519',     '2019-03-12', 1),
    ('AF087LM', 'Mercedes-Benz OH 1621',     '2020-07-01', 1),
    ('AF215QR', 'Scania K270',               '2021-02-15', 1),
    ('AG334TZ', 'Agrale MT 17.0',            '2022-05-20', 1),
    ('AG501BV', 'Iveco Tector 170E22',       '2022-11-08', 1),
    ('AH129CD', 'Volkswagen 17.230 OD',      '2023-04-17', 1),
    ('AH376NS', 'Mercedes-Benz OF 1722',     '2023-09-25', 1),
    ('AA845XY', 'Mercedes-Benz OF 1115',     '2016-06-30', 0),
    ('AB067HG', 'Iveco Tector 170E22',       '2017-10-02', 1),
    ('AC912JR', 'Scania K280',               '2018-04-19', 1),
    ('AD233WE', 'Volkswagen 17.230 OD',      '2024-01-29', 1),
    ('AD778MP', 'Agrale MT 17.0',            '2024-08-12', 1),
    ('AF640ZK', 'Mercedes-Benz OH 1621',     '2020-12-03', 1),
    ('AB419FT', 'Agrale MA 15.0',            '2018-01-22', 0);

    -- Numeracion 1..n por orden de alta: los ids reales (IDENTITY) no se asumen
    SELECT ROW_NUMBER() OVER (ORDER BY num_chofer) AS seq, num_chofer, activo INTO #Chofer FROM dbo.Chofer;
    SELECT ROW_NUMBER() OVER (ORDER BY num_interno) AS seq, num_interno, disponible INTO #Interno FROM dbo.Interno;

    -- =====================================================
    -- CRONOGRAMAS: una serie diaria por linea y turno.
    -- Lunes a viernes: turno manana (ruta de ida) y tarde (ruta de vuelta). Fines de semana: solo manana, menos frecuencia.
    -- =====================================================
    SELECT * INTO #Tpl FROM (VALUES
        (1, 0, 'RUT-001', N'matutino',   CAST('06:00' AS TIME), CAST('09:30' AS TIME), 45, 90, 10, 1),
        (2, 0, 'RUT-002', N'vespertino', CAST('16:30' AS TIME), CAST('20:00' AS TIME), 45, 90, 10, 0),
        (3, 1, 'RUT-003', N'matutino',   CAST('05:30' AS TIME), CAST('09:00' AS TIME), 45, 90, 15, 1),
        (4, 1, 'RUT-004', N'vespertino', CAST('16:00' AS TIME), CAST('19:30' AS TIME), 45, 90, 15, 0),
        (5, 2, 'RUT-005', N'matutino',   CAST('06:00' AS TIME), CAST('09:30' AS TIME), 45, 90, 12, 1),
        (6, 2, 'RUT-006', N'vespertino', CAST('16:30' AS TIME), CAST('20:00' AS TIME), 45, 90, 12, 0)
    ) AS t(tpl, linea, id_ruta, turno, horaInicio, horaFin, frecSemana, frecFinde, descanso, operaFinde);

    -- 1900-01-01 fue lunes: (dias % 7) >= 5 es sabado o domingo, sin depender del idioma del servidor
    SELECT ROW_NUMBER() OVER (ORDER BY d.fecha, t.tpl) AS ck,
           d.fecha, t.tpl, t.linea, t.id_ruta, t.turno, t.horaInicio, t.horaFin,
           CASE WHEN d.finde = 1 THEN t.frecFinde ELSE t.frecSemana END AS frec,
           t.descanso, r.tiempoEstimadoMin,
           N'Servicio ' + t.turno + N' ' + r.descripcion + N' (' + CONVERT(NVARCHAR(10), d.fecha, 103) + N')' AS descripcion
    INTO #Cron
    FROM (SELECT DATEADD(DAY, n - 76, @Hoy) AS fecha,
                 CASE WHEN DATEDIFF(DAY, '19000101', DATEADD(DAY, n - 76, @Hoy)) % 7 >= 5 THEN 1 ELSE 0 END AS finde
          FROM #N WHERE n <= 80) d
    JOIN #Tpl t ON (d.finde = 0 OR t.operaFinde = 1)
    JOIN dbo.Ruta r ON r.id = t.id_ruta;

    DECLARE @MapCron TABLE (ck INT PRIMARY KEY, id INT NOT NULL);

    MERGE dbo.Cronograma AS tgt
    USING #Cron AS src ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (descripcion, fechaValidez, horaInicio, horaFin, frecuenciaMinutos, tiempoDescansoMinutos, id_ruta)
        VALUES (src.descripcion, src.fecha, src.horaInicio, src.horaFin, src.frec, src.descanso, src.id_ruta)
    OUTPUT src.ck, inserted.id INTO @MapCron (ck, id);

    -- =====================================================
    -- SALIDAS: una cada "frecuencia" minutos entre horaInicio y horaFin (igual que la autogeneracion de la aplicacion)
    -- =====================================================
    SELECT c.ck, c.fecha, c.linea, c.id_ruta, c.turno,
           DATEADD(MINUTE, (n.n - 1) * c.frec, c.horaInicio) AS hSal,
           DATEADD(MINUTE, (n.n - 1) * c.frec + c.tiempoEstimadoMin, c.horaInicio) AS hLle
    INTO #Sal0
    FROM #Cron c
    JOIN #N n ON n.n <= 20 AND (n.n - 1) * c.frec < DATEDIFF(MINUTE, c.horaInicio, c.horaFin);

    SELECT s.*,
           ROW_NUMBER() OVER (ORDER BY s.ck, s.hSal) AS sk,
           ROW_NUMBER() OVER (PARTITION BY s.fecha, s.linea ORDER BY s.hSal, s.ck) - 1 AS k,
           DATEDIFF(DAY, '20000101', s.fecha) AS dayIdx
    INTO #Sal1
    FROM #Sal0 s;

    -- Cada linea tiene su propio grupo de 6 choferes y 4 internos que rotan por dia. Con una salida cada 45 minutos y
    -- viajes de 75 a 90, nunca hay mas de 2 viajes simultaneos de la misma linea: no hay solapamientos.
    SELECT s.*,
           s.linea * 6 + ((s.k + s.dayIdx) % 6) + 1 AS choferSeq,
           s.linea * 4 + ((s.k + s.dayIdx) % 4) + 1 AS internoSeq,
           ABS(CHECKSUM(s.sk, 1) % 100)  AS h1,
           ABS(CHECKSUM(s.sk, 2) % 1000) AS h2,
           ABS(CHECKSUM(s.sk, 3) % 100)  AS h3,
           ABS(CHECKSUM(s.sk, 4) % 100)  AS h4,
           ABS(CHECKSUM(s.sk, 5) % 100)  AS h5,
           ABS(CHECKSUM(s.sk, 6) % 100)  AS h6,
           ABS(CHECKSUM(s.sk, 7) % 100)  AS h7
    INTO #Sal2
    FROM #Sal1 s;

    -- Retraso de llegada en minutos: depende del chofer (algunos cronicamente tardones), del turno (la tarde es peor),
    -- de la linea La Plata (autopista), y de "dias complicados" (uno de cada 13: lluvia, cortes, etc.)
    SELECT s.*,
           CASE WHEN s.fecha < @Hoy AND (s.h3 < 3 OR (s.dayIdx % 13 = 0 AND s.h3 < 10)) THEN 1 ELSE 0 END AS susp,
           CASE WHEN s.fecha >= DATEADD(DAY, -2, @Hoy) AND s.fecha < @Hoy AND s.h6 < 35 THEN 1 ELSE 0 END AS pend,
           CASE
               WHEN r.score < 60  THEN (s.h2 % 6) - 2
               WHEN r.score < 80  THEN 4 + s.h2 % 6
               WHEN r.score < 94  THEN 10 + s.h2 % 15
               WHEN r.score < 108 THEN 25 + s.h2 % 25
               ELSE 50 + s.h2 % 40
           END AS delay
    INTO #Sal3
    FROM #Sal2 s
    CROSS APPLY (SELECT s.h1
                        + CASE s.choferSeq % 6 WHEN 1 THEN 20 WHEN 3 THEN 8 WHEN 5 THEN -12 ELSE 0 END
                        + CASE WHEN s.turno = N'vespertino' THEN 8 ELSE 0 END
                        + CASE WHEN s.linea = 1 THEN 6 ELSE 0 END
                        + CASE WHEN s.dayIdx % 13 = 0 THEN 18 ELSE 0 END AS score) r;

    INSERT INTO dbo.Salida (id_cronograma, num_interno, num_chofer, horaSalidaTeorica, horaLlegadaTeorica,
                            horaSalidaReal, horaLlegadaReal, estaSuspendida, motivoSuspension)
    SELECT m.id,
           CASE WHEN s.susp = 1 THEN NULL
                WHEN s.fecha > @Hoy AND s.h5 < 20 THEN NULL          -- futuro: aun sin interno asignado
                WHEN s.fecha >= @Hoy AND i.disponible = 0 THEN NULL   -- interno en taller: hay que reasignar
                ELSE i.num_interno END,
           CASE WHEN s.susp = 1 THEN NULL
                WHEN s.fecha > @Hoy AND s.h4 < 30 THEN NULL          -- futuro: aun sin chofer asignado
                ELSE c.num_chofer END,
           s.hSal,
           s.hLle,
           CASE WHEN s.susp = 0 AND s.fecha < @Hoy
                THEN DATEADD(MINUTE, CASE WHEN s.delay > 0 THEN s.delay * 6 / 10 ELSE 0 END, s.hSal) END,
           CASE WHEN s.susp = 0 AND s.fecha < @Hoy AND s.pend = 0
                THEN DATEADD(MINUTE, s.delay, s.hLle) END,           -- pend = 1: llegada de ayer/anteayer aun sin registrar
           s.susp,
           CASE WHEN s.susp = 1 THEN CHOOSE(1 + s.h2 % 6,
                N'Falla mecánica del interno',
                N'Corte de ruta por manifestación',
                N'Condiciones climáticas adversas',
                N'Ausencia del chofer asignado',
                N'Paro de transporte',
                N'Obras en la vía pública') END
    FROM #Sal3 s
    JOIN @MapCron m ON m.ck = s.ck
    JOIN #Interno i ON i.seq = s.internoSeq
    JOIN #Chofer c ON c.seq = s.choferSeq;

    -- =====================================================
    -- SANCIONES
    --  a) por retrasos graves (25 minutos o mas) ya auditados: se sanciona a 6 de cada 10, con los minutos reales en el motivo
    --  b) por otras faltas, repartidas en los ultimos 180 dias y mas frecuentes en los primeros choferes
    -- =====================================================
    INSERT INTO dbo.Sancion (num_chofer, fecha, motivo)
    SELECT c.num_chofer, s.fecha,
           N'Retraso de ' + CAST(s.delay AS NVARCHAR(10)) + N' minutos en la salida de las '
           + LEFT(CONVERT(VARCHAR(8), s.hSal, 108), 5) + N' (ruta ' + s.id_ruta + N')'
           + CASE WHEN s.delay >= 50 THEN N'. Demora superior a 50 minutos: falta grave.' ELSE N'.' END
    FROM #Sal3 s
    JOIN #Chofer c ON c.seq = s.choferSeq
    WHERE s.susp = 0 AND s.pend = 0 AND s.fecha < @Hoy AND s.delay >= 25 AND s.h7 < 60;

    INSERT INTO dbo.Sancion (num_chofer, fecha, motivo)
    SELECT c.num_chofer,
           DATEADD(DAY, -ABS(CHECKSUM(x.n, 12) % 180), @Hoy),
           CHOOSE(1 + ABS(CHECKSUM(x.n, 13) % 10),
                N'Conducción imprudente: exceso de velocidad en tramo urbano',
                N'Incumplimiento del uniforme reglamentario',
                N'Trato inadecuado a un pasajero',
                N'No registró la llegada en el horario establecido',
                N'Ausencia injustificada a su turno',
                N'Uso del teléfono celular durante la conducción',
                N'Salida de la parada sin respetar el tiempo de ascenso',
                N'No realizó el control previo del interno',
                N'Desvío no autorizado del recorrido',
                N'Reincidencia en llegadas tardías')
    FROM (SELECT n FROM #N WHERE n <= 60) x
    JOIN #Chofer c ON c.seq = 1 + (ABS(CHECKSUM(x.n, 10) % 18) * ABS(CHECKSUM(x.n, 11) % 18)) / 17;

    -- =====================================================
    -- CARGAS DE COMBUSTIBLE: 30 por interno cada ~2,5 dias (los internos en taller dejan de cargar en el ultimo tramo).
    -- Kilometraje siempre creciente, precio por litro con tendencia al alza, ~3% de cargas anuladas.
    -- =====================================================
    INSERT INTO dbo.CargaCombustible (num_interno, fechaHora, litrosCargados, precioPorLitro, kilometrajeActual, anulada, motivoAnulacion)
    SELECT i.num_interno,
           DATEADD(HOUR, 8 + (j.n - 1) * 60 + (i.seq * 7 + j.n * 5) % 9, CAST(DATEADD(DAY, -75, @Hoy) AS DATETIME)),
           CAST(210 + (ABS(CHECKSUM(i.seq, j.n) % 8100)) / 100.0 AS DECIMAL(10, 2)),
           CAST(1080 + (j.n - 1) * 7.5 + (i.seq % 3) * 12 + (ABS(CHECKSUM(i.seq, j.n, 5) % 500)) / 100.0 AS DECIMAL(10, 2)),
           90000 + (i.seq * 23117) % 210000 + (j.n - 1) * 700 + ((j.n - 1) * i.seq * 13) % 60,
           x.anulada,
           CASE WHEN x.anulada = 1 THEN CHOOSE(1 + ABS(CHECKSUM(i.seq, j.n, 8) % 3),
                N'Error de carga: litros mal ingresados',
                N'Comprobante duplicado',
                N'Carga registrada en el interno equivocado') END
    FROM #Interno i
    JOIN #N j ON j.n <= 30 AND NOT (i.disponible = 0 AND j.n > 26)
    CROSS APPLY (SELECT CASE WHEN ABS(CHECKSUM(i.seq, j.n, 9) % 100) < 3 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS anulada) x;

    -- =====================================================
    -- TALLER: revisiones (8 por interno en 180 dias, mas una reciente en los internos no disponibles),
    -- ordenes de reparacion para las que detectan una falla, y su detalle de insumos y mano de obra.
    -- Los costos varian por interno y suben con el tiempo; algunos insumos recientes aun no tienen costo (NULL).
    -- =====================================================
    SELECT * INTO #TipoRev FROM (VALUES
        (1,  N'Revisión técnica periódica sin observaciones',      0, NULL, NULL, NULL, NULL),
        (2,  N'Control de niveles y fluidos',                      0, NULL, NULL, NULL, NULL),
        (3,  N'Inspección de neumáticos y alineación',             0, NULL, NULL, NULL, NULL),
        (4,  N'Control de sistema eléctrico y luces',              0, NULL, NULL, NULL, NULL),
        (5,  N'Chequeo del aire acondicionado',                    0, NULL, NULL, NULL, NULL),
        (6,  N'Revisión general previa a VTV',                     0, NULL, NULL, NULL, NULL),
        (7,  N'Falla en el sistema de frenos',                     1, N'Desgaste de pastillas y discos de freno',                 1,  2,    NULL),
        (8,  N'Pérdida de aceite en el motor',                     1, N'Reemplazo de juntas y retenes del motor',                 7,  8,    3),
        (9,  N'Ruido anormal en la caja de cambios',               1, N'Reparación de la caja de cambios',                        19, 20,   NULL),
        (10, N'Falla en la suspensión neumática',                  1, N'Reemplazo de fuelles y amortiguadores',                   11, 10,   NULL),
        (11, N'Sobrecalentamiento del motor',                      1, N'Reparación del sistema de refrigeración',                 13, 14,   12),
        (12, N'Falla en el aire acondicionado',                    1, N'Recarga y reparación del equipo de aire acondicionado',   17, 18,   NULL),
        (13, N'Falla en batería y alternador',                     1, N'Reemplazo de batería y alternador',                       15, 16,   NULL),
        (14, N'Desgaste del embrague',                             1, N'Reemplazo del kit de embrague',                           9,  NULL, NULL)
    ) AS t(tipo, descripcion, requiere, motivo, item1, item2, item3);

    SELECT * INTO #Insumo FROM (VALUES
        (1,  N'Pastillas de freno (juego)',                70000,  95000,  1,  2),
        (2,  N'Disco de freno',                            120000, 160000, 1,  2),
        (3,  N'Aceite de motor 15W40 (litro)',             7000,   9500,   18, 24),
        (4,  N'Filtro de aceite',                          18000,  26000,  1,  2),
        (5,  N'Filtro de aire',                            22000,  32000,  1,  2),
        (6,  N'Filtro de combustible',                     20000,  30000,  1,  2),
        (7,  N'Kit de juntas de motor',                    180000, 260000, 1,  1),
        (8,  N'Retén de cigüeñal',                         45000,  65000,  1,  2),
        (9,  N'Kit de embrague',                           450000, 620000, 1,  1),
        (10, N'Amortiguador',                              95000,  140000, 2,  4),
        (11, N'Fuelle de suspensión neumática',            150000, 210000, 2,  4),
        (12, N'Bomba de agua',                             130000, 190000, 1,  1),
        (13, N'Radiador',                                  380000, 520000, 1,  1),
        (14, N'Termostato',                                25000,  38000,  1,  1),
        (15, N'Batería 12V 180Ah',                         210000, 290000, 1,  2),
        (16, N'Alternador',                                340000, 470000, 1,  1),
        (17, N'Compresor de aire acondicionado',           520000, 720000, 1,  1),
        (18, N'Gas refrigerante (kg)',                     28000,  40000,  2,  4),
        (19, N'Kit de reparación de caja de cambios',      600000, 900000, 1,  1),
        (20, N'Aceite de caja de cambios (litro)',         9000,   12000,  8,  12),
        (21, N'Mano de obra (hora)',                       22000,  30000,  3,  12)
    ) AS t(id, insumo, costoMin, costoMax, qMin, qMax);

    SELECT ROW_NUMBER() OVER (ORDER BY q.iseq, q.m) AS rk, q.iseq, q.num_interno, q.fecha, q.tipo
    INTO #Rev
    FROM (
        SELECT i.seq AS iseq, m.n AS m, i.num_interno,
               DATEADD(DAY, -180 + (m.n - 1) * 24 + ((i.seq * 3 + m.n * 7) % 10), @Hoy) AS fecha,
               1 + ABS(CHECKSUM(i.seq, m.n, 77) % 14) AS tipo
        FROM #Interno i JOIN #N m ON m.n <= 8
        UNION ALL
        SELECT i.seq, 99, i.num_interno, DATEADD(DAY, -2, @Hoy), CASE WHEN i.seq = 8 THEN 9 ELSE 11 END
        FROM #Interno i WHERE i.disponible = 0
    ) q;

    DECLARE @MapRev TABLE (rk INT PRIMARY KEY, id INT NOT NULL);

    MERGE dbo.RevisionTaller AS tgt
    USING (SELECT r.rk, r.num_interno, r.fecha, t.descripcion, t.requiere
           FROM #Rev r JOIN #TipoRev t ON t.tipo = r.tipo) AS src ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (num_interno, fecha, descripcion, reparacionRequerida)
        VALUES (src.num_interno, src.fecha, src.descripcion, src.requiere)
    OUTPUT src.rk, inserted.id INTO @MapRev (rk, id);

    DECLARE @MapOrden TABLE (rk INT PRIMARY KEY, id INT NOT NULL);

    MERGE dbo.OrdenReparacion AS tgt
    USING (SELECT r.rk, mr.id AS id_revision, t.motivo
           FROM #Rev r
           JOIN #TipoRev t ON t.tipo = r.tipo AND t.requiere = 1
           JOIN @MapRev mr ON mr.rk = r.rk) AS src ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (id_revision, motivoReparacion)
        VALUES (src.id_revision, src.motivo)
    OUTPUT src.rk, inserted.id INTO @MapOrden (rk, id);

    -- Detalle: mano de obra + hasta 3 insumos propios de cada tipo de falla
    INSERT INTO dbo.DetalleOrdenReparacion (id_ordenReparacion, insumo, cantidad, costoUnitario)
    SELECT mo.id,
           ins.insumo,
           ins.qMin + ABS(CHECKSUM(r.rk, ins.id) % (ins.qMax - ins.qMin + 1)),
           CASE WHEN ins.id <> 21 AND r.fecha >= DATEADD(DAY, -10, @Hoy) AND ABS(CHECKSUM(r.rk, ins.id, 3) % 100) < 25
                THEN NULL
                ELSE CAST(ROUND(
                        (ins.costoMin + (ins.costoMax - ins.costoMin) * (ABS(CHECKSUM(r.rk, ins.id, 4) % 101)) / 100.0)
                        * (1 + 0.25 * (180 + DATEDIFF(DAY, @Hoy, r.fecha)) / 180.0)      -- inflacion: lo reciente cuesta mas
                        * (90 + (r.iseq * 17) % 50) / 100.0                               -- cada interno tiene su propio desgaste
                    , -2) AS INT)
           END
    FROM #Rev r
    JOIN #TipoRev t ON t.tipo = r.tipo AND t.requiere = 1
    JOIN @MapOrden mo ON mo.rk = r.rk
    CROSS APPLY (VALUES (0, 21), (1, t.item1), (2, t.item2), (3, t.item3)) AS s(slot, itemId)
    JOIN #Insumo ins ON ins.id = s.itemId;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
