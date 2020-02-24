CREATE TABLE RouteInfo (
	RouteId uniqueidentifier NOT NULL PRIMARY KEY,
	RouteDistance int NOT NULL,
	NumbStudents int NOT NULL,
	StudentIdArray varchar(255) NOT NULL,
	GeocodedAdressArray varchar(255) NOT NULL
	);
