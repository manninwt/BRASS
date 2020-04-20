using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BRASS.Models
{
    public class Drivers
    {
        [Key]
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string Condition { get; set; }
        public string StartAddress { get; set; }
        public string StartCity { get; set; }
        public string StartZipCode { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
        public int BusId { get; set; }
    }
}
