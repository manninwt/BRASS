using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRASS.DataAccessLayer;
using BRASS.Models;

namespace BRASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            var busNumbers = _context.Buses.Select(x => new { Text = "Bus Number " + x.BusNumb.ToString(), Value = x.BusId });

            var model = new Buses();
            List<SelectListItem> buses = new List<SelectListItem>();
            foreach (var busNumber in busNumbers)
            {
                buses.Add(new SelectListItem { Text = busNumber.Value.ToString(), Value = busNumber.Text });
            }
            model.BusNumberList = new SelectList(buses, "Text", "Value");

            return View(model);
        }
    }
}
