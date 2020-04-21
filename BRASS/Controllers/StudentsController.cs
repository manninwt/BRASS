using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRASS.DataAccessLayer;
using BRASS.Models;
using BRASS.Models.PageModels;

namespace BRASS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            using (var context = _context)
            {
                var RouteNumbers = context.Routes.Select(x => new { Text = "Route: " + x.RouteId.ToString(), Value = x.RouteId });

                var model = new StudentPage();
                List<SelectListItem> routes = new List<SelectListItem>();
                foreach (var route in RouteNumbers)
                {
                    routes.Add(new SelectListItem { Text = route.Value.ToString(), Value = route.Text });
                }
                model.RouteList = new SelectList(routes, "Text", "Value");

                var studentQuery = context.Students.AsNoTracking().ToList();
                model.StudentList = studentQuery;

                var RouteStopsQuery = context.RouteStops.AsNoTracking().ToList();
                model.RouteStopsList = RouteStopsQuery;

                var RoutesQuery = context.Routes.AsNoTracking().ToList();
                model.RoutesList = RoutesQuery;

                var BusesQuery = context.Buses.AsNoTracking().ToList();
                model.BusList = BusesQuery;

                return View(model);
            }
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,ParentFirstName,ParentLastName,ParentPhoneNumber,StreetAddress,City,ZipCode,StopId,Longitude,Lattitude")] GeocodedStudent students)
        {
            Students student = new Students();
            if (ModelState.IsValid)
            {
                student.FirstName = students.FirstName;
                student.LastName = students.LastName;
                student.ParentFirstName = students.ParentFirstName;
                student.ParentLastName = students.ParentLastName;
                student.ParentPhoneNumber = students.ParentPhoneNumber;
                student.StreetAddress = students.StreetAddress;
                student.City = students.City;
                student.ZipCode = students.ZipCode;

                int studentStopId = 0;
                int studentId = 0;
                using (var context = _context)
                {
                    RouteStops studentStop = new RouteStops();
                    studentStop.Lattitude = students.Lattitude;
                    studentStop.Longitude = students.Longitude;

                    context.Add(studentStop);
                    context.Add(student);
                    context.SaveChanges();

                    studentStopId = studentStop.StopId;
                    studentId = students.StudentId;

                    student.StopId = studentStopId;
                    context.Update(student);
                    context.SaveChanges();
                    await context.SaveChangesAsync();
                    
                }

                return RedirectToAction(nameof(Index));

                //Need to figure out a way to assign the studentStopId to the newly created routeStops object
            }
            return View(student);
        }

        public async Task<IActionResult> CreateStop()
        {
            using(var context = _context)
            {
                var student = context.Students.AsNoTracking()
                    .Where(x => x.StopId == 0)
                    .FirstOrDefault();

                var stop = context.RouteStops.AsNoTracking()
                    .Where(x => x.Lattitude == 0 && x.Longitude == 0 && x.StopNumber == 0)
                    .FirstOrDefault();

                Students studentUpdate = new Students();
                studentUpdate.StudentId = student.StudentId;
                studentUpdate.StopId = stop.StopId;

                context.Update(studentUpdate);
                context.SaveChanges();
            }

            return View();
        }

        public async void CreateStop(int studentStopId, int studentId)
        {
            using (var context = _context)
            {
                var student = context.Students.AsNoTracking()
                    .Where(x => x.StudentId == studentId)
                    .FirstOrDefault();

                student.StopId = studentStopId;

                context.SaveChanges();
                await context.SaveChangesAsync();
            }
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,ParentFirstName,ParentLastName,ParentPhoneNumber,StreetAddress,City,ZipCode")] Students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        public ActionResult GetStudentsOnRoute(int id)
        {
            if (id == 0)
            {
                using (var context = _context)
                {
                    var model = new StudentPage();

                    var routeStopIds = context.RouteStops.AsNoTracking()
                        .Select(x => x.StopId)
                        .ToList();

                    var studentList = context.Students.AsNoTracking()
                        .Where(x => routeStopIds.Contains(x.StopId))
                        .ToList();

                    return Json(studentList);
                }
            }
            else
            {
                using (var context = _context)
                {
                    var model = new StudentPage();

                    var routeStopIds = context.RouteStops.AsNoTracking()
                        .Where(x => x.RouteId == id)
                        .Select(x => x.StopId)
                        .ToList();

                    var studentList = context.Students.AsNoTracking()
                        .Where(x => routeStopIds.Contains(x.StopId))
                        .ToList();

                    model.StudentList = studentList;

                    return Json(studentList);
                }
            }
        }
    }
}
