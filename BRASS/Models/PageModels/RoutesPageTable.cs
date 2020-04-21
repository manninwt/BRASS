using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models.PageModels
{
    public class RoutesPageTable
    {
        [NotMapped]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public int StopNumber { get; set; }
        public int BusNumber { get; set; }
    }
}
