﻿ALTER TABLE RoutePoints
ADD CONSTRAINT FK_RoutePoints_RouteId
FOREIGN KEY (RouteId) REFERENCES Route(RouteId);
