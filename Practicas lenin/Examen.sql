/*CREATE TABLE Zoologico
(
id_zoo            int,
Nombre            varchar(80),
ciudad            varchar(50),
pais              varchar(50),
presupuesto_anual int,
tamaño_m2         int,
Cantidad_Animales int
);

CREATE TABLE EspecieAnimales
(
id_especies         number(6),
Nombre_vulgar       varchar(80),
Nombre_cientifico   varchar(100),
familia             varchar(80), 
peligroEx           varchar(10)
);

CREATE TABLE LosAnimales
(
id_zoo            int,
no_identificacion int,
especie           varchar(80),
sexo              varchar(20),
año_nacimiento    number(20),
pais_de_origen    varchar(100),
continente        varchar(20)
);*/

ALTER TABLE Zoologico ADD CONSTRAINT pk_Zoologico_id_zoo
PRIMARY KEY(id_zoo);

ALTER TABLE EspecieAnimales ADD CONSTRAINT pk_EspecieAnimales_id_especies 
PRIMARY KEY(id_especies );

ALTER TABLE Zoologico ADD CONSTRAINT pk_zoologico_id_zoo
FOREIGN Key(id_zoo) References Animales (id_zoo);
