using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class Routes
    {
        [Key]
        public int RouteId { get; set; }
        public char RouteGroup { get; set; }
        public int BusId { get; set; }
        public int UnassignedStudents { get; set; }
        public int UnassignedBuses { get; set; }
    }
}
