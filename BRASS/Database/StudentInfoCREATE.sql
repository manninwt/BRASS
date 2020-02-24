CREATE TABLE StudentInfo (
    StudentId uniqueidentifier NOT NULL PRIMARY KEY,
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
	ParentFName varchar(255) NOT NULL,
    ParentLName varchar(255) NOT NULL,
	ParentPhoneNumber char(10) NOT NULL,
	StudentStreetAddress varchar(255) NOT NULL,
	City varchar(255) NOT NULL,
	ZipCode char(5) NOT NULL
);
