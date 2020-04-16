CREATE TABLE RouteStops (
	StopId int IDENTITY(1,1) NOT NULL,
	StopNumber int,
	Longitude decimal(17,15),
	Lattitude decimal(17,15),
	RouteId int,
	PRIMARY KEY (StopId)
	);
