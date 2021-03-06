USE ControleAcesso

CREATE TABLE USUARIOS(
	ID INT NOT NULL,
	NOME VARCHAR(100) NOT NULL
	CONSTRAINT PK_USUARIOS_ID
	PRIMARY KEY (ID)
)

CREATE TABLE AMBIENTES(
	ID INT NOT NULL,
	NOME VARCHAR(100) NOT NULL
	CONSTRAINT PK_AMBIENTES_ID
	PRIMARY KEY (ID)
)

CREATE TABLE AMBIENTES_USUARIOS(
	ID INT IDENTITY	NOT NULL,
	ID_AMBIENTES INT NOT NULL,
	ID_USUARIOS INT NOT NULL
	CONSTRAINT PK_AMBIENTES_USUARIOS_ID
	PRIMARY KEY (ID),
	CONSTRAINT FK_AMBIENTES_USUARIOS_AMBIENTES
		FOREIGN KEY (ID_AMBIENTES) REFERENCES AMBIENTES(ID),
	CONSTRAINT FK_AMBIENTES_USUARIOS_USUARIOS
		FOREIGN KEY (ID_USUARIOS) REFERENCES USUARIOS(ID),
)

CREATE TABLE LOGS(
	ID INT IDENTITY	NOT NULL,
	DT_ACESSO DATETIME  NOT NULL,
	TIPO_ACESSO TINYINT NOT NULL,
	ID_USUARIOS INT NOT NULL,
	ID_AMBIENTES INT NOT NULL
	CONSTRAINT PK_LOGS_ID
	PRIMARY KEY (ID),
	CONSTRAINT FK_LOGS_AMBIENTES
		FOREIGN KEY (ID_AMBIENTES) REFERENCES AMBIENTES(ID),
	CONSTRAINT FK_LOGS_USUARIOS
		FOREIGN KEY (ID_USUARIOS) REFERENCES USUARIOS(ID),
)
