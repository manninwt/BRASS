CREATE TABLE School (
	SchoolId int IDENTITY(1,1) NOT NULL,
	SchoolName varchar(255) NOT NULL,
	SchoolAddress varchar(255) NOT NULL,
	SchoolCity varchar(255) NOT NULL,
	SchoolZipCode varchar(255) NOT NULL,
	Longitude decimal(17, 15) NOT NULL,
	Lattitude decimal(17, 15) NOT NULL,
	NumbBuses int NOT NULL,
	ArrivalTime time NOT NULL,
	DepartureTime time NOT NULL,
	RouteGroup char(255) NOT NULL,
	PRIMARY KEY (SchoolId)
	);