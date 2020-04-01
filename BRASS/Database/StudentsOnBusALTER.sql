ALTER TABLE StudentsOnBus
ADD CONSTRAINT FK_StudentsOnBus_BusId FOREIGN KEY (BusId) REFERENCES Bus(BusId),
	CONSTRAINT FK_StudentsOnBus_StudentId FOREIGN KEY (StudentId) REFERENCES Student(StudentId);
