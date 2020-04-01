CREATE TABLE Bus (
	BusId int IDENTITY(1,1) NOT NULL,
	BusNumb int NOT NULL,
	Capacity int NOT NULL,
	Condition BIT NOT NULL,
	Handicap BIT NOT NULL,
	DriverId int,
	RouteId int,
	PRIMARY KEY (BusId)
	);