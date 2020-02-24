CREATE TABLE SchoolInfo (
	SchoolId uniqueidentifier NOT NULL PRIMARY KEY,
	SchoolAddress varchar(255) NOT NULL,
	GeocodedAddress varchar(255) NOT NULL,
	NumbBuses int NOT NULL,
	ArriveTime time NOT NULL,
	DepartureTime time NOT NULL
	);
