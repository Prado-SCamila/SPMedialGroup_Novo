USE SPmed;
Go


INSERT INTO Especialidades (nome)
VALUES ('Acupuntura'),
	('Anestesiologia'),
	('Angiologia'),
	('Cardiologia'),
	('Cirurgia cardiovascular'),
	('Cirurgia da mão'),
	('Cirurgia do aparelho digestivo'),
	('Cirurgia geral'),
	('Cirurgia pediátrica'),
	('Cirurgia plástica'),
	('Cirurgia toráxica'),
	('Cirurgia vascular'),
	('Dermatologia'),
	('Radioterapia'),
	('Urologia'),
	('Pediatria'),
	('Psiquiatria');
	Go


	INSERT INTO TipoUsuarios (tituloTipoUsuario)
	VALUES  ('Administrador'),
			('Médico'),
			('Paciente');
	Go


INSERT INTO Clinica (nomeFantasia,cnpj,razaoSocial,endereco)
VALUES ('Clínica Possarle','86.400.902/0001-30','SP Medical Group','Av. Barão Limeira, 532, São Paulo, SP');
Go

INSERT INTO Usuarios (nome,email,senha)
VALUES  ('Camila','admin@adm.com','adm123'),
		('Ricardo Lemos','ricardo.lemos@spmedicalgroup.com.br','lemos123'),
		('Roberto Possarle','roberto.possarle@spmedicalgroup.com.br','possarle123'),
		('Helena Strada','helena.souza@spmedicalgroup.com.br','strada123'),
		('Ligia','ligia@gmail.com','ligia123'),
		('Alexandre','alexandre@gmail.com','ale123'),
		('Fernando','fernando@gmail.com','fer123'),
		('Henrique','henrique@gmail.com','rique123'),
		('João','joao@hotmail.com','joao123'),
		('Bruno','bruno@gmail.com','bruno123'),
		('Mariana','mariana@outlook.com','mari123');
Go
		

INSERT INTO Medicos (crm)
VALUES ('54356-SP'),
	   ('53452-SP'),
	   ('65463-SP');
Go

INSERT INTO Prontuarios (dataNascimento, telefone, rg,cpf,endereco)
VALUES ('13/10/1983','1134567654','43522543-5','94839859000','Rua Estado de Israel 240, São Paulo, Estado de São Paulo, 04022-000'),
		('23/07/2001','11987656543','32654345-7','73556944057','Av. Paulista, 1578 - Bela Vista, São Paulo - SP, 01310-200'),
		('10/10/1978','11972084453','54636525-3','16839338002','Av. Ibirapuera - Indianópolis, 2927,  São Paulo - SP, 04029-200'),
		('13/10/1985','1134566543','54366362-5','14332654765','R. Vitória, 120 - Vila Sao Jorge, Barueri - SP, 06402-030'),
		('27/08/1975','1176566377','432544444-1','91305348010','R. Ver. Geraldo de Camargo, 66 - Santa Luzia, Ribeirão Pires - SP, 09405-380'),
		('21/03/1972','11954368769','54566266-7','79799299004','Alameda dos Arapanés, 945 - Indianópolis, São Paulo - SP, 04524-001'),
		('05/03/2018','','54566266-8','13771913039','R Sao Antonio, 232 - Vila Universal, Barueri - SP, 06407-140');
Go

INSERT INTO Consultas (idProntuario, idMedico, dataConsulta, idSituacao)
VALUES  (7,3,'20/01/2020 15:00',3),
		(2,2,'06/01/2020 10:00',1),
		(3,2,'07/02/2020 11:00',3),
		(2,2,'06/02/2018 10:00',3),
		(4,1,'07/02/2019 11:00',1),
		(7,3,'08/03/2020 15:00',2),
		(4,1,'09/03/2020 11:00',2);
Go



INSERT INTO Situacao (situacao)
VALUES  ('Cancelada'),
        ('Agendada'),
		('Concluida');

----Definindo de qual tipo cada usuário é
UPDATE Usuarios
	   SET idTipoUsuario = 2
	   WHERE idUsuario>=2 AND idUsuario<=4;
	
UPDATE Usuarios
	SET idTipoUsuario =1
	WHERE Usuarios.nome ='Camila';

UPDATE Usuarios
	SET idTipoUsuario = 3
	WHERE idUsuario>=5 AND idUsuario<=11;

	---relacionando cada médico com seu idUsuario
UPDATE Medicos
	SET idUsuario=2
	WHERE idMedico=1;

UPDATE Medicos
	SET idUsuario=3
	WHERE idMedico=2;

UPDATE Medicos
	SET idUsuario=4
	WHERE idMedico=3;

	SELECT *  FROM Medicos;

UPDATE Medicos
SET idClinica=1;

------incluindo o idespecialidade na tabela medicos

UPDATE Medicos
	SET idEspecialidade=2
	WHERE idUsuario=2;

UPDATE Medicos
	SET idEspecialidade=17
	WHERE idUsuario=3;

	UPDATE Medicos
	SET idEspecialidade=16
	WHERE idUsuario=4;

--------incluindo o id prontuario na tabela consultas
UPDATE Consultas
SET idProntuario=7 
WHERE idConsulta=1;

UPDATE Consultas
SET idProntuario=4
WHERE idConsulta=5;

UPDATE Consultas
SET idProntuario=2
WHERE idConsulta=2;

UPDATE Consultas
SET idProntuario=3
WHERE idConsulta=3;

UPDATE Consultas
SET idProntuario=2
WHERE idConsulta=4;

UPDATE Consultas
SET idProntuario=7
WHERE idConsulta=6;

UPDATE Consultas
SET idProntuario=4
WHERE idConsulta=7;

-----incluindo o idMedico na tabela consultas

UPDATE Consultas
SET idMedico=3
WHERE idConsulta=1;

UPDATE Consultas
SET idMedico=2
WHERE idConsulta>=2 AND idConsulta<=4;

UPDATE Consultas
SET idMedico=1
WHERE idConsulta=5;

UPDATE Consultas
SET idMedico=3
WHERE idConsulta=6;

UPDATE Consultas
SET idMedico=1
WHERE idConsulta=7;


----incluindo os idUsuario na tabela Prontuario

UPDATE Prontuarios
SET idUsuario=5
WHERE idProntuario=1;

UPDATE Prontuarios
SET idUsuario=6
WHERE idProntuario=2;

UPDATE Prontuarios
SET idUsuario=7
WHERE idProntuario=3;

UPDATE Prontuarios
SET idUsuario=8
WHERE idProntuario=4;

UPDATE Prontuarios
SET idUsuario=9
WHERE idProntuario=5;

UPDATE Prontuarios
SET idUsuario=10
WHERE idProntuario=6;

UPDATE Prontuarios
SET idUsuario=11
WHERE idProntuario=7;

ALTER TABLE Consultas
DROP COLUMN Situacao;

