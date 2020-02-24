CREATE TABLE ConversionAddress (
	StudentId uniqueidentifier,
	GeocodedAddress varchar(255) NOT NULL,
	FOREIGN KEY (StudentId) REFERENCES StudentInfo(StudentId)
	);