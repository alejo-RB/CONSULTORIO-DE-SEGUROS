CREATE DATABASE CONSULTORIO_SEGUROS;
GO

USE CONSULTORIO_SEGUROS;
GO

CREATE TABLE Seguros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreSeguro VARCHAR(100) NOT NULL,
    CodigoSeguro VARCHAR(20) NOT NULL,
    SumaAsegurada DECIMAL(18, 2) NOT NULL,
    Prima DECIMAL(18, 2) NOT NULL
);
GO

CREATE TABLE Asegurados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cedula VARCHAR(20) NOT NULL,
    NombreCliente VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Edad INT NOT NULL
);
GO

CREATE TABLE AseguradosSeguros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AseguradoId INT NOT NULL,
    SeguroId INT NOT NULL,
    FOREIGN KEY (AseguradoId) REFERENCES Asegurados(Id),
    FOREIGN KEY (SeguroId) REFERENCES Seguros(Id),
    CONSTRAINT UQ_AseguradosSeguros UNIQUE (AseguradoId, SeguroId)
);
GO


