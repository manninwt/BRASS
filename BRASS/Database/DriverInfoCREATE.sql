CREATE TABLE DriverInfo (
	DriverId uniqueidentifier NOT NULL PRIMARY KEY,
	FirstName varchar(255) NOT NULL,
	LastName varchar(255) NOT NULL,
	Condition BIT,
	BusId int,
	StartAddress varchar(255) NOT NULL,
	FOREIGN KEY (BusId) REFERENCES BusInfo(BusId)
	);
