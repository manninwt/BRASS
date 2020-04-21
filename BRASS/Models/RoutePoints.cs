using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class RoutePoints
    {
        [Key]
        public int pointId { get; set; }
        public int RouteId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
    }
}
