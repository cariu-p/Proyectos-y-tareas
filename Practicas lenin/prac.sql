create table Alumnos
(
Matricula varchar(13),
Nombre varchar(80),
Direccion varchar(100),
edad int,
semestre int
);

create table Maestros
(
clave_mto varchar(10),
Nombre    varchar(80),
Horas     int,
profesion varchar(50),
salario   number(8,2)
);

create table Materias
(
clave_materia varchar(10),
Nombre        varchar(30),
No_credito    int,
Plan_estudios varchar(100)
);

create table Grupos
(
Id_grupos     varchar(3),
Matricula     varchar(13),
clave_mto     varchar(10),
clave_materia varchar(13),
Edificio      varchar(20),
Aula          varchar(4)
);
/*Llave primaria*/
ALTER TABLE Alumnos
ADD CONSTRAINT PK_Alumnos_Matricula
PRIMARY KEY (Matricula);

ALTER TABLE Maestros
ADD CONSTRAINT PK_Maestros_clave_mto
PRIMARY KEY (clave_mto);

ALTER TABLE Materias
ADD CONSTRAINT PK_Materias_clave_materia
PRIMARY KEY (clave_materia);

ALTER TABLE Grupos
ADD CONSTRAINT PK_Grupos_Id_grupos
PRIMARY KEY (Id_grupos);
/*Llave foranea*/
ALTER TABLE Grupos ADD CONSTRAINT PK_Grupos_Matricula
FOREIGN KEY (Matricula)REFERENCES Alumnos(Matricula);

ALTER TABLE Grupos ADD CONSTRAINT PK_Grupos_clave_mto
FOREIGN KEY (clave_mto)REFERENCES Maestros(clave_mto);

ALTER TABLE Grupos ADD CONSTRAINT PK_Grupos_clave_materia
FOREIGN KEY (clave_materia)REFERENCES Materias(clave_materia);