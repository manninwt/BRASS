﻿ALTER TABLE Driver
ADD CONSTRAINT FK_Driver_BusId
FOREIGN KEY (BusId) REFERENCES Bus(BusId);
