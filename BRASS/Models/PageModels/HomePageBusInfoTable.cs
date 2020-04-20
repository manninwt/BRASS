using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models.PageModels
{
    public class HomePageBusInfoTable
    {
        public string DriverName { get; set; }
        public int RouteNumber { get; set; }
        public int NumberOfStudents { get; set; }
        public string Status { get; set; }
        public string Handicap { get; set; }
        public int BusNumber { get; set; }
    }
}
