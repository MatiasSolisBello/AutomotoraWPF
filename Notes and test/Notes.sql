 -- 1. Elimine la tabla "agenda" si existe:
 if object_id('agenda') is not null
  drop table agenda;


-- 2. Crear tabla agenda
create table agenda(
  apellido varchar(30),
  nombre varchar(20),
  domicilio varchar(30),
  telefono varchar(11)
);


-- 3. Visualice las tablas existentes
exec sp_tables @table_owner='dbo'


-- 4. Visualice la estructura de la tabla "agenda"
exec sp_columns agenda;


-- 5. Elimine la tabla.
drop table agenda;


-- 6. ingresa datos a tabla agenda
insert into agenda (apellido, nombre, domicilio, telefono) values ('Moreno','Alberto','Colon 123','4234567');
insert into agenda (apellido,nombre, domicilio, telefono) values ('Torres','Juan','Avellaneda 135','4458787');

select * from agenda;


-- Trabaje con la tabla "libros" que almacena los datos de los libros de su propia biblioteca.
if object_id('libros') is not null
  drop table libros;


create table libros(
	titulo varchar(20), 
	autor varchar(30),
	editorial varchar(15),
	precio float,		--decimal
	cantidad integer	--entero
);


exec sp_tables @table_owner='dbo'


-- Insertar datos
insert into libros (titulo,autor,editorial, precio, cantidad) 
	values ('El aleph','Borges','Planeta', 15.23, 50);
insert into libros (titulo,autor,editorial, precio, cantidad) 
	values ('Martin Fierro','Jose Hernandez','Emece', 15.23, 50);
insert into libros (titulo,autor,editorial, precio, cantidad) 
	values ('Aprenda PHP','Mario Molina','Emece', 15.23, 50);
insert into libros (titulo,autor,editorial, precio, cantidad) 
	values ('La Batalla Cultural','Laje','Planeta', 25.00, 50);


select * from libros;

-- Seleccionamos libros cuyo autor sea distindo de Borges
select * from libros where autor<>'Borges';


-- Seleccionamos registros cuyo precio sea mamor a 20 pesos,
-- mostrar solo titulo y precio
select titulo, precio from libros
where precio >=20.00;


-- Borrar registros
delete from libros where editorial='Planeta';

--Actualizar registros
update libros set autor='Agustin Laje' where autor='Laje';

-- Valor Null
select * from libros where precio is null;


--Concatenar
select titulo+'-'+autor+'-'+editorial from libros;


--Tipo de dato (fecha y hora)
if object_id('empleados') is not null
  drop table empleados;

create table empleados(
  nombre varchar(20),
  documento char(8),
  fechahora datetime,
  fecha date
);
go

-- Seteamos el formato de la fecha para que guarde día, mes y año:
set dateformat dmy;

insert into empleados values('Ana Gomez','22222222','12-01-1980', '12-01-1980');
insert into empleados values('Bernardo Huerta','23333333','15-03-81', '15-03-81');
insert into empleados values('Carla Juarez','24444444','20/05/1983', '20/05/1983');
insert into empleados values('Daniel Lopez','25555555','2.5.1990', '2.5.1990');

select * from empleados;