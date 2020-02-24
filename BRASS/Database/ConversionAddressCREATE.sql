CREATE TABLE ConversionAddress (
	StudentId int,
	GeocodedAddress varchar(255) NOT NULL,
	FOREIGN KEY (StudentId) REFERENCES StudentInfo(StudentId)
	);