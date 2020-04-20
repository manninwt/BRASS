using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRASS.DataAccessLayer;
using BRASS.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;

namespace BRASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;
        IConfiguration _iConfiguration;


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

        public HomeController(SchoolContext context, IConfiguration iConfiguration)

        {
            _context = context;
            _iConfiguration = iConfiguration;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            using (var context = _context)
            {
                var busNumbers = context.Buses.Select(x => new { Text = "Bus Number " + x.BusNumb.ToString(), Value = x.BusId });

                var model = new HomePage();
                List<SelectListItem> homePage = new List<SelectListItem>();
                foreach (var busNumber in busNumbers)
                {
                    homePage.Add(new SelectListItem { Text = busNumber.Value.ToString(), Value = busNumber.Text });
                }
                model.BusNumberList = new SelectList(homePage, "Text", "Value");

                var ActiveBuses = context.Buses.AsNoTracking()
                    .Where(x => x.Condition == "FUNCTIONAL")
                    .Count();

                var InactiveBuses = context.Buses.AsNoTracking()
                    .Where(x => x.Condition == "NOT FUNCTIONAL")
                    .Count();

                var ActiveDrivers = context.Drivers.AsNoTracking()
                    .Where(x => x.Condition == "ACTIVE")
                    .Count();

                var InactiveDrivers = context.Drivers.AsNoTracking()
                    .Where(x => x.Condition == "INACTIVE")
                    .Count();

                model.ActiveBuses = ActiveBuses;
                model.ActiveDrivers = ActiveDrivers;
                model.InactiveBuses = InactiveBuses;
                model.InactiveDrivers = InactiveDrivers;


                return View(model);
            }
        }

        public ActionResult GetSelectedValue(int id)
        {
            if (id == 0)
            {
                using (var context = _context)
                {

                    var routeQuery = context.Routes.AsNoTracking().ToList();

                    var routeId = routeQuery.FirstOrDefault<Routes>().RouteId;

                    var routePointsList = context.RoutePoints.AsNoTracking()
                        .Where(x => x.RouteId == routeId)
                        .ToList();

                    return Json(routePointsList);
                }
            }
            else
            {
                using (var context = _context)
                {

                    var routeQuery = context.Routes.AsNoTracking()
                        .Where(x => x.BusId == id)
                        .ToList();

                    var routeId = routeQuery.FirstOrDefault<Routes>().RouteId;

                    var routePointsList = context.RoutePoints.AsNoTracking()
                        .Where(x => x.RouteId == routeId)
                        .ToList();

                    return Json(routePointsList);
                }
            }
        }

        public ActionResult GetAllStopValues()
        {
            using (var context = _context)
            {
                var routeStops = context.RouteStops.AsNoTracking().ToList();

                return Json(routeStops);
            }
        }

        public ActionResult GetSchoolValues()
        {
            using (var context = _context)
            {
                var school = context.School.AsNoTracking().ToList();

                return Json(school);
            }
        }

        public ActionResult GetDriverValues()
        {
            using (var context = _context)
            {
                var drivers = context.Drivers.AsNoTracking().ToList();

                return Json(drivers);
            }
        }

        public ActionResult GetBusValues()
        {
            using (var context = _context)
            {
                var bus = context.Buses.AsNoTracking().ToList();

                return Json(bus);
            }
        }

        public ActionResult GetAccessTokenCred()
        {
            var AccessTokenInfo = new AccessToken(_iConfiguration);
            return Json(AccessTokenInfo);
        }

        public class AccessToken
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string grant_type { get; set; }

            public AccessToken(IConfiguration _iConfiguration)
            {
                this.client_id = _iConfiguration["AccessTokenInfo:client_id"];
                this.client_secret = _iConfiguration["AccessTokenInfo:client_secret"];
                this.grant_type = _iConfiguration["AccessTokenInfo:grant_type"];
            }
        }
    }
}
