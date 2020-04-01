using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class Buses
    {
        [Key]
        public int BusId { get; set; }
        public int BusNumb { get; set; }
        public int Capacity { get; set; }
        public string Condition { get; set; }
        public string Handicap { get; set; }
        public int DriverId { get; set; }
        public int RouteId { get; set; }
    }
}
