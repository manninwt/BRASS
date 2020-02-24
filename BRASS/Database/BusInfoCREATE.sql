CREATE TABLE BusInfo (
	BusId uniqueidentifier NOT NULL PRIMARY KEY,
	BusNumb int NOT NULL,
	Capacity int NOT NULL,
	DriverId int,
	Condition BIT NOT NULL,
	RouteId int,
	Handicap BIT NOT NULL,
	FOREIGN KEY (RouteId) REFERENCES RouteInfo(RouteId)
	);


ALTER TABLE BusInfo
ADD CONSTRAINT FK_DriverId
FOREIGN KEY (DriverId) REFERENCES DriverInfo(DriverId);