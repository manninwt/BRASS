using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models.PageModels
{
    public class BusPage
    {
        public int BusId { get; set; }
        public int BusNumb { get; set; }
        public int Capacity { get; set; }
        public string Condition { get; set; }
        public string Handicap { get; set; }
        public string DriverName { get; set; }
        public int RouteId { get; set; }
    }
}
