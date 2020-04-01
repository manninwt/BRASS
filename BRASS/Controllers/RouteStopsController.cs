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
    public class RouteStopsController : Controller
    {
        private readonly SchoolContext _context;

        public RouteStopsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: RouteStops
        public async Task<IActionResult> Index()
        {
            return View(await _context.RouteStops.ToListAsync());
        }

        // GET: RouteStops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStops = await _context.RouteStops
                .FirstOrDefaultAsync(m => m.StopId == id);
            if (routeStops == null)
            {
                return NotFound();
            }

            return View(routeStops);
        }

        // GET: RouteStops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteStops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StopId,StopNumber,Longitude,Lattitude,RouteId")] RouteStops routeStops)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeStops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routeStops);
        }

        // GET: RouteStops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStops = await _context.RouteStops.FindAsync(id);
            if (routeStops == null)
            {
                return NotFound();
            }
            return View(routeStops);
        }

        // POST: RouteStops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StopId,StopNumber,Longitude,Lattitude,RouteId")] RouteStops routeStops)
        {
            if (id != routeStops.StopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeStops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteStopsExists(routeStops.StopId))
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
            return View(routeStops);
        }

        // GET: RouteStops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStops = await _context.RouteStops
                .FirstOrDefaultAsync(m => m.StopId == id);
            if (routeStops == null)
            {
                return NotFound();
            }

            return View(routeStops);
        }

        // POST: RouteStops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeStops = await _context.RouteStops.FindAsync(id);
            _context.RouteStops.Remove(routeStops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteStopsExists(int id)
        {
            return _context.RouteStops.Any(e => e.StopId == id);
        }
    }
}
