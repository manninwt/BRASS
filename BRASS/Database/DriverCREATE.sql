CREATE TABLE Driver (
	DriverId int IDENTITY(1,1) NOT NULL,
	FirstName varchar(255) NOT NULL,
	LastName varchar(255) NOT NULL,
	DriverPhoneNumber char(10) NOT NULL,
	Condition varchar(255) NOT NULL,
	StartAddress varchar(255) NOT NULL,
	StartCity varchar(255) NOT NULL,
	StartZipCode varchar(5) NOT NULL,
	Longitude decimal(17,15),
	Lattitude decimal(17,15),
	BusId int,
	PRIMARY KEY (DriverId)
	);
