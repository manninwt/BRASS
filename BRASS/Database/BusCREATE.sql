CREATE TABLE Bus (
	BusId int IDENTITY(1,1) NOT NULL,
	BusNumb int NOT NULL,
	Capacity int NOT NULL,
	Condition varchar(255) NOT NULL,
	Handicap varchar(255) NOT NULL,
	DriverId int,
	RouteId int,
	PRIMARY KEY (BusId)
	);