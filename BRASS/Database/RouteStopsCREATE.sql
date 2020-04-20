CREATE TABLE RouteStops (
	StopId int IDENTITY(1,1) NOT NULL,
	StopNumber int,
	Longitude decimal(18,15),
	Lattitude decimal(18,15),
	RouteId int,
	PRIMARY KEY (StopId)
	);
