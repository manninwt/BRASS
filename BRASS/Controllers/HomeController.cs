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

                Console.WriteLine(routeStops);
                return Json(routeStops);
            }
        }

        public ActionResult GetMultiRouteInfo()
        {
            using (var context = _context)
            {
                var routeStops = context.RouteStops.AsNoTracking().ToList();
                var school = context.School.AsNoTracking().ToList();
                var drivers = context.Drivers.AsNoTracking().ToList();
                var bus = context.Buses.AsNoTracking().ToList();

                return Json(new MultiRouteInfo(Json(routeStops), Json(school), Json(drivers), Json(bus)));
            }
        }

        public class MultiRouteInfo
        {
            private JsonResult routeStops;
            private JsonResult school;
            private JsonResult drivers;
            private JsonResult bus;

            public MultiRouteInfo(JsonResult routeStops, JsonResult school, JsonResult drivers, JsonResult bus)
            {
                this.routeStops = routeStops;
                this.school = school;
                this.drivers = drivers;
                this.bus = bus;
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
