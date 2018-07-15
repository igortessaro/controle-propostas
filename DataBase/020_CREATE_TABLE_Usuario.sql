CREATE TABLE Usuario (
    UsuarioCodigo INT NOT NULL IDENTITY(1,1),
    Nome VARCHAR(255) NOT NULL,
    Cpf VARCHAR(11) NOT NULL,
	Email VARCHAR(150) NOT NULL,
	DataNascimento DATE NOT NULL,
    ChaveAcesso VARCHAR(MAX),
	Perfil INT NOT NULL
    CONSTRAINT PK_Usuario PRIMARY KEY (UsuarioCodigo),
	CONSTRAINT UC_Usuario UNIQUE (UsuarioCodigo,Cpf, Email),
);

GO

INSERT INTO 
	dbo.Usuario(Nome, Cpf, Email, DataNascimento, ChaveAcesso, Perfil)
VALUES
	('Administrador',				'12345678901', 'admin@admin.com',			'1980-01-01', NULL, 3),
	('Fulano da Silva',				'11111111111', 'fulano.silva@cp.com',		'1980-01-01', NULL, 0),
	('Beltrano Santos',				'22222222222', 'beltrano.santos@cp.com',	'1980-01-01', NULL, 0),
	('Ciclano Martins',				'33333333333', 'ciclano.martins@cp.com',	'1980-01-01', NULL, 1),
	('Valcreide da Silva Santos',	'44444444444', 'valcreide.silva@cp.com',	'1980-01-01', NULL, 2),
	('Florentina de Nazare',		'55555555555', 'florentina.nazare@cp.com',	'1980-01-01', NULL, 2)