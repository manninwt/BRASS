using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models.PageModels
{
    public class StudentPage
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int StopId { get; set; }
        public int RouteId { get; set; }
        public char RouteGroup { get; set; }
        public int BusId { get; set; }

        [NotMapped]
        public SelectList RouteList { get; set; }
        public List<Students> StudentList { get; set; }
    }
}
