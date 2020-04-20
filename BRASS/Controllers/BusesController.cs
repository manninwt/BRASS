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
    public class BusesController : Controller
    {
        private readonly SchoolContext _context;

        public BusesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            using (var context = _context)
            {
                List<BusPage> busList = new List<BusPage>();
                var busQuery = context.Buses.AsNoTracking().ToList();

                foreach (var bus in busQuery)
                {

                    var driverFirstName = context.Drivers.AsNoTracking()
                        .Where(x => x.DriverId == bus.DriverId)
                        .Select(x => x.FirstName)
                        .FirstOrDefault();

                    var driverLastName = context.Drivers.AsNoTracking()
                        .Where(x => x.DriverId == bus.DriverId)
                        .Select(x => x.LastName)
                        .FirstOrDefault();

                    var model = new BusPage();
                    model.BusId = bus.BusId;
                    model.BusNumb = bus.BusNumb;
                    model.Capacity = bus.Capacity;
                    model.Condition = bus.Condition;
                    model.DriverName = driverFirstName + " " + driverLastName;
                    model.Handicap = bus.Handicap;
                    model.RouteId = bus.RouteId;
                    busList.Add(model);
                }

                return View(busList);
            }
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses
                .FirstOrDefaultAsync(m => m.BusId == id);
            if (buses == null)
            {
                return NotFound();
            }

            return View(buses);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusId,BusNumb,Capacity,Condition,Handicap,DriverId,RouteId")] Buses buses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buses);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses.FindAsync(id);
            if (buses == null)
            {
                return NotFound();
            }
            return View(buses);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusId,BusNumb,Capacity,Condition,Handicap,DriverId,RouteId")] Buses buses)
        {
            if (id != buses.BusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusesExists(buses.BusId))
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
            return View(buses);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses
                .FirstOrDefaultAsync(m => m.BusId == id);
            if (buses == null)
            {
                return NotFound();
            }

            return View(buses);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buses = await _context.Buses.FindAsync(id);
            _context.Buses.Remove(buses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusesExists(int id)
        {
            return _context.Buses.Any(e => e.BusId == id);
        }
    }
}
