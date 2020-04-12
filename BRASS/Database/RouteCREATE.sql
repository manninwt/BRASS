CREATE TABLE Route (
	RouteId int IDENTITY(1,1) NOT NULL,
	RouteGroup char NOT NULL,
	BusId int,
	PRIMARY KEY (RouteId)
	);
