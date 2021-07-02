USE SPmed;
Go


-------TABELA ESPECIALIDADES-------
SELECT * FROM Especialidades;
Go

---------TABELA MÉDICOS----------

SELECT crm AS CRM, Usuarios.nome, Usuarios.email, Especialidades.nome AS Especialidades,Clinica.nomeFantasia AS Clinica, Clinica.cnpj AS CNPJ,Clinica.razaoSocial,Clinica.endereco FROM Usuarios
INNER JOIN Medicos
ON Medicos.idUsuario = Usuarios.idUsuario
INNER JOIN Clinica
ON Clinica.idClinica = Medicos.idClinica
INNER JOIN Especialidades
ON Medicos.idEspecialidade = Especialidades.idEspecialidade;
Go


---------TABELA PRONTUÁRIOS---------
SELECT Usuarios.nome AS Nome_do_Paciente,Usuarios.email AS Email,dataNascimento AS Data_de_Nascimento ,telefone AS Telefone,rg AS RG,cpf AS CPF , endereco  AS Endereço FROM Usuarios
INNER JOIN Prontuarios
ON Usuarios.idUsuario = Prontuarios.idUsuario;
Go

----------TABELA CONSULTAS-----------

SELECT Usuarios.nome AS Nome_do_Paciente,Usuarios.nome AS Médicos, Consultas.dataConsulta AS Data_da_Consulta ,Consultas.situacao AS Situação FROM Usuarios
INNER JOIN Prontuarios
ON Usuarios.idUsuario = Prontuarios.idUsuario
INNER JOIN Consultas
ON Consultas.idProntuario = Prontuarios.idProntuario
INNER JOIN Medicos
ON Medicos.idMedico = Consultas.idMedico;
Go