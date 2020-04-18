using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRASS.DataAccessLayer;
using BRASS.Models;
using NHibernate.Impl;

namespace BRASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        private IEnumerable<SelectListItem> GetAllBuses()
        {
            IEnumerable<SelectListItem> list = _context.Buses.Select(s => new SelectListItem
            {
                Selected = false,
                Text = "Bus Number" + s.BusNumb.ToString(),
                Value = s.BusId.ToString()
            });

            return list;
        }

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            var busNumbers = _context.Buses.Select(x => new { Text = "Bus Number " + x.BusNumb.ToString(), Value = x.BusId });

            var model = new HomePage();
            List<SelectListItem> homePage = new List<SelectListItem>();
            foreach (var busNumber in busNumbers)
            {
                homePage.Add(new SelectListItem { Text = busNumber.Value.ToString(), Value = busNumber.Text });
            }
            model.BusNumberList = new SelectList(homePage, "Text", "Value");

            return View(model);
        }

        public ActionResult GetSelectedValue(int id)
        {
            using (var context = _context)
            {
                var routeQuery = from r in context.Routes
                            where r.BusId == id
                            select r;

                var routeId = routeQuery.FirstOrDefault<Routes>().RouteId;


                var routePointsList = context.RoutePoints.AsNoTracking()
                    .Where(x => x.RouteId == routeId)
                    .ToList();
                
                return Json(routePointsList);
            }
        }
    }
}
