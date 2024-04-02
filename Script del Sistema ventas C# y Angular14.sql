create database DBVENTA
go

use DBVENTA
go

set dateformat dMy

create table Rol(
idRol int primary key identity(1,1),
nombre varchar(50),
fechaRegistro datetime default getdate()
)

go

create table Menu(
idMenu int primary key identity(1,1),
nombre varchar(50),
icono varchar(50),
url varchar(50)
)

go

create table MenuRol(
idMenuRol int primary key identity(1,1),
idMenu int references Menu(idMenu),
idRol int references Rol(idRol)
)

go


create table Usuario(
idUsuario int primary key identity(1,1),
nombreCompleto varchar(100),
correo varchar(40),
idRol int references Rol(idRol),
clave varchar(40),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

go

create table Categoria(
idCategoria int primary key identity(1,1),
nombre varchar(50),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

go

create table Producto (
idProducto int primary key identity(1,1),
nombre varchar(100),
idCategoria int references Categoria(idCategoria),
stock int,
precio decimal(10,2),
esActivo bit default 1,
fechaRegistro datetime default getdate()
)

go

create table NumeroDocumento(
idNumeroDocumento int primary key identity(1,1),
ultimo_Numero int not null,
fechaRegistro datetime default getdate()
)
go

create table Venta(
idVenta int primary key identity(1,1),
numeroDocumento varchar(40),
tipoPago varchar(50),
total decimal(10,2),
fechaRegistro datetime default getdate()
)
go

create table DetalleVenta(
idDetalleVenta int primary key identity(1,1),
idVenta int references Venta(idVenta),
idProducto int references Producto(idProducto),
cantidad int,
precio decimal(10,2),
total decimal(10,2)
)

go

--COMANDO PARA SOLUCIONAR EL PROBLEMA DE AUTORIZACIÓN PARA GENERAR DIAGRAMAS
ALTER AUTHORIZATION ON DATABASE::DBVENTA TO sa;



insert into Rol(nombre) values
('Administrador'),
('Empleado'),
('Supervisor')

go

insert into Usuario(nombreCompleto,correo,idRol,clave) values 
('codigo estudiante','code@example.com',1,'123')
go

INSERT INTO Categoria(nombre,esActivo) values
('Laptops',1),
('Monitores',1),
('Teclados',1),
('Auriculares',1),
('Memorias',1),
('Accesorios',1)

go

insert into Producto(nombre,idCategoria,stock,precio,esActivo) values
('laptop samsung book pro',1,20,2500,1),
('laptop lenovo idea pad',1,30,2200,1),
('laptop asus zenbook duo',1,30,2100,1),
('monitor teros gaming te-2',2,25,1050,1),
('monitor samsung curvo',2,15,1400,1),
('monitor huawei gamer',2,10,1350,1),
('teclado seisen gamer',3,10,800,1),
('teclado antryx gamer',3,10,1000,1),
('teclado logitech',3,10,1000,1),
('auricular logitech gamer',4,15,800,1),
('auricular hyperx gamer',4,20,680,1),
('auricular redragon rgb',4,25,950,1),
('memoria kingston rgb',5,10,200,1),
('ventilador cooler master',6,20,200,1),
('mini ventilador lenono',6,15,200,1)
go

insert into Menu(nombre,icono,url) values
('DashBoard','dashboard','/pages/dashboard'),
('Usuarios','group','/pages/usuarios'),
('Productos','collections_bookmark','/pages/productos'),
('Venta','currency_exchange','/pages/venta'),
('Historial Ventas','edit_note','/pages/historial_venta'),
('Reportes','receipt','/pages/reportes')
go

--menus para administrador
insert into MenuRol(idMenu,idRol) values
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1)
go

--menus para empleado
insert into MenuRol(idMenu,idRol) values
(4,2),
(5,2)
go

--menus para supervisor
insert into MenuRol(idMenu,idRol) values
(3,3),
(4,3),
(5,3),
(6,3)
go

insert into numerodocumento(ultimo_Numero,fechaRegistro) values
(0,getdate())