CREATE TABLE Parada (
	id VARCHAR(20) PRIMARY KEY,
	descripcion VARCHAR(255) NOT NULL,
	localidad VARCHAR(200) NOT NULL,
	direccion VARCHAR(200) NOT NULL,
	habilitada BIT NOT NULL
);

CREATE TABLE Ruta (
	id VARCHAR(20) PRIMARY KEY,
	descripcion VARCHAR(255) NOT NULL,
	sentido VARCHAR(20) NOT NULL,
	distanciaTotalKM INT NOT NULL,
	tiempoEstimadoMin INT NOT NULL
);

CREATE TABLE Recorrido_Ruta(
	id_ruta VARCHAR(20) NOT NULL,
	id_parada VARCHAR(20) NOT NULL,
	orden INT NOT NULL,
	CONSTRAINT PK_recorrido_ruta PRIMARY KEY (id_ruta, id_parada),
	CONSTRAINT FK_Ruta_Recorrido FOREIGN KEY (id_ruta) REFERENCES Ruta(id),
	CONSTRAINT FK_Parada_Ruta FOREIGN KEY (id_parada) REFERENCES Parada(id)
);