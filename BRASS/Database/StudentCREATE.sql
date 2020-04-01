CREATE TABLE Student (
    StudentId int IDENTITY(1,1) NOT NULL,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
	ParentFirstName varchar(255) NOT NULL,
    ParentLastName varchar(255) NOT NULL,
	ParentPhoneNumber char(10) NOT NULL,
	StreetAddress varchar(255) NOT NULL,
	City varchar(255) NOT NULL,
	ZipCode char(5) NOT NULL,
	StopId int,
	PRIMARY KEY (StudentId)
);
