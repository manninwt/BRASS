using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BRASS.Models;
using BRASS.Services;
using System.Data;

namespace BRASS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Student()
        {
            return View();
        }

        public IActionResult Drivers()
        {
            return View();
        }

        public IActionResult School()
        {
            return View();
        }

        public IActionResult Routes()
        {
            return View();
        }

        public IActionResult Misc()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult addStudent(string studentFirstName, string studentLastName, string parentFirstName, string parentLastName, string parentPhoneNumber, string studentStreetAddress, string city, string zipCode)
        {
            Services.StudentsService.AddStudent(studentFirstName, studentLastName, parentFirstName, parentLastName, parentPhoneNumber, studentStreetAddress, city, zipCode);
            return RedirectToAction("Students");
        }
    }
}
