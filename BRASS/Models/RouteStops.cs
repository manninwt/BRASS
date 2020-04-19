using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class RouteStops
    {
        [Key]
        public int StopId { get; set; }
        public int StopNumber { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
        public int RouteId { get; set; }
    }
}
