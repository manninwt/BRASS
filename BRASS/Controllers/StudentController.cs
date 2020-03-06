using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Threading.Tasks;
using BRASS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BRASS.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Student()
        {
            string connectionStringName = "Data Source=DESKTOP-UG9TO2R;Initial Catalog=BRASS;Integrated Security=True";
            using (var session = NHibernateHelper.OpenSession(connectionStringName))
            {
                var students = session.Query<Student>().ToList();
                ViewBag.students = students;
                return View("~/Views/Home/Student.cshtml");
            }
        }
    }
}