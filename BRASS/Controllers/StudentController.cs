using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRASS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BRASS.Controllers
{
    public class StudentController : Controller
    {
        Student db = new Student();

        public IActionResult Index()
        {
            var students = from m in db.FirstName select m;

            return View(students.ToList());
        }
    }
}