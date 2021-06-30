CREATE DATABASE SPmed;
Go


USE SPmed;
Go

CREATE TABLE TipoUsuarios
(
idTipoUsuario INT PRIMARY KEY IDENTITY,
tituloTipoUsuario VARCHAR(200)NOT NULL,
);
Go

CREATE TABLE Usuarios
(
idUsuario INT PRIMARY KEY IDENTITY,
idTipoUsuario INT FOREIGN KEY REFERENCES TipoUsuarios (idTipoUsuario),
nome VARCHAR(200)NOT NULL,
email VARCHAR(300) NOT NULL,
senha VARCHAR(300)NOT NULL,
);
Go

CREATE TABLE Prontuarios
(
idProntuario INT PRIMARY KEY IDENTITY,
idUsuario INT FOREIGN KEY REFERENCES Usuarios (idUsuario),
dataNascimento DATE NOT NULL,
telefone BIGINT UNIQUE,
rg CHAR(500) UNIQUE NOT NULL,
cpf CHAR(500) UNIQUE NOT NULL,
endereco VARCHAR(400),
);
Go

CREATE TABLE Especialidades
(
idEspecialidade INT PRIMARY KEY IDENTITY,
nome VARCHAR(200)NOT NULL,
);
Go

CREATE TABLE Clinica
(
idClinica INT PRIMARY KEY IDENTITY,
nomeFantasia VARCHAR(200) NOT NULL,
cnpj CHAR(500) UNIQUE NOT NULL,
razaoSocial VARCHAR(200) UNIQUE NOT NULL,
endereco VARCHAR(300)NOT NULL,
);
Go

CREATE TABLE Medicos
(
idMedico INT PRIMARY KEY IDENTITY,
idUsuario INT FOREIGN KEY REFERENCES Usuarios (idUsuario),
idEspecialidade INT FOREIGN KEY REFERENCES Especialidades (idEspecialidade),
idClinica INT FOREIGN KEY REFERENCES Clinica (idClinica),
crm CHAR(300) UNIQUE NOT NULL,
);
Go

CREATE TABLE Consultas
(
idConsulta INT PRIMARY KEY IDENTITY,
idProntuario INT FOREIGN KEY REFERENCES Prontuarios (idProntuario),
idMedico INT FOREIGN KEY REFERENCES Medicos (idMedico),
dataConsulta DATETIME NOT NULL,
idSituacao INT FOREIGN KEY REFERENCES Situacao(idSituacao),
descricao VARCHAR(300) ,
);
Go



CREATE TABLE Situacao
(
idSituacao INT PRIMARY KEY IDENTITY,
situacao VARCHAR(200) DEFAULT 'agendada'
);