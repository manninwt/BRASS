using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models
{
    public class HomePage
    {
        [Key]
        public int BusId { get; set; }
        public int BusNumb { get; set; }
        public int Capacity { get; set; }
        public string Condition { get; set; }
        public string Handicap { get; set; }
        public int DriverId { get; set; }
        public int RouteId { get; set; }

        [NotMapped]
        public SelectList BusNumberList { get; set; }
        public List<Buses> BusList { get; set; }
        public List<RoutePoints> RoutePoints { get; set; }
        public List<Buses> ActiveBuses { get; set; }
        public List<Buses> InactiveBuses { get; set; }
        public List<Drivers> ActiveDrivers { get; set; }
        public List<Drivers> InactiveDrivers { get; set; }

    }
}
