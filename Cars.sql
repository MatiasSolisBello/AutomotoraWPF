/*
CREATE TABLE dbo.Cars(
	Id INT NOT NULL PRIMARY KEY identity,
    LicencePlate varchar(10) NOT NULL unique,
	Year INT NOT NULL,
	DateFabrication DATETIME NOT NULL,
	New BIT NOT NULL,
	Transmissions BIT NOT NULL,
	ModelId INT NOT NULL
)
GO

CREATE TABLE dbo.Brands(
	Id INT NOT NULL PRIMARY KEY identity,
	Name varchar(50) NOT NULL
)
GO

CREATE TABLE dbo.Models(
	Id INT NOT NULL PRIMARY KEY identity,
	Name varchar(50) NOT NULL,
	BrandId INT NOT NULL
)
GO
*/

ALTER TABLE Models add foreign key(BrandId) references Brands(Id);
ALTER TABLE Cars add foreign key(ModelId) references Models(Id);

SET IDENTITY_INSERT dbo.Brands ON
INSERT dbo.Brands (Id, Name) VALUES (1, 'Toyota')
INSERT dbo.Brands (Id, Name) VALUES (2, 'Chevrolet')
INSERT dbo.Brands (Id, Name) VALUES (3, 'Suzuki')
INSERT dbo.Brands (Id, Name) VALUES (4, 'Ford')
SET IDENTITY_INSERT dbo.Brands OFF
GO

SET IDENTITY_INSERT dbo.Models ON
INSERT dbo.Models (Id, Name, BrandId) VALUES (1, 'Yaris Sedan XLI C MT', 1)
INSERT dbo.Models (Id, Name, BrandId) VALUES (2, 'Yaris Sedan LEY MD', 1)
INSERT dbo.Models (Id, Name, BrandId) VALUES (3, 'New Silverado', 2)
INSERT dbo.Models (Id, Name, BrandId) VALUES (4, 'Onix Sedan', 2)
INSERT dbo.Models (Id, Name, BrandId) VALUES (5, 'Ñuniki', 3)
INSERT dbo.Models (Id, Name, BrandId) VALUES (6, 'Edge', 3)
INSERT dbo.Models (Id, Name, BrandId) VALUES (7, 'Explored', 3)
SET IDENTITY_INSERT dbo.Models OFF
GO

SET IDENTITY_INSERT dbo.Cars ON
INSERT dbo.Cars (Id, LicencePlate, Year, DateFabrication, New, Transmissions, ModelId) 
VALUES
(1, '11111', 2011, '01/03/2021', 'FALSE', 'FALSE', 1)

SET IDENTITY_INSERT dbo.Cars OFF
GO


SELECT * FROM dbo.Cars;
SELECT * FROM dbo.Brands;
SELECT * FROM dbo.Models;