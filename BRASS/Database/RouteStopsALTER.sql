﻿ALTER TABLE RouteStops
ADD CONSTRAINT FK_RouteStops_RouteId
FOREIGN KEY (RouteId) REFERENCES Route(RouteId);