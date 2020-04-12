CREATE TABLE RouteStops (
	StopId int IDENTITY(1,1) NOT NULL,
	StopNumber int NOT NULL,
	Longitude decimal(17,15) NOT NULL,
	Lattitude decimal(17,15) NOT NULL,
	RouteId int NOT NULL,
	PRIMARY KEY (StopId)
	);
