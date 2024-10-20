/*CREATE TABLE Clientes
(
No_cliente Number(10),
Nombre      varchar(50),
Direccion   varchar(50),
telefono    number(10)
);

CREATE TABLE Pedidos
(
No_pedidos   Number(10),
fecha        date,
No_cliente   Number(10),
Total        Number(2)
);

ALTER TABLE Clientes ADD CONSTRAINT pk_Clientes_No_cliente
PRIMARY KEY(No_cliente);

ALTER TABLE Pedidos ADD CONSTRAINT pk_Pedidos_No_Pedidos
PRIMARY KEY(No_Pedidos);*/

ALTER TABLE Pedidos ADD CONSTRAINT pk_Pedidos_No_cliente
FOREIGN KEY(No_cliente) REFERENCES Clientes (No_cliente);


