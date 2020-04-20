using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class RoutePoints
    {
        [Key]
        public int RouteId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
    }
}
