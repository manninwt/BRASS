using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCity { get; set; }
        public string SchoolZipCode { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
        public int NumbBuses { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public char RouteGroup { get; set; }
    }
}
