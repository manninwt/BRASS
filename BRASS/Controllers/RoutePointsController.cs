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
    public class RoutePointsController : Controller
    {
        private readonly SchoolContext _context;

        public RoutePointsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: RoutePoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoutePoints.ToListAsync());
        }

        // GET: RoutePoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoints = await _context.RoutePoints
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (routePoints == null)
            {
                return NotFound();
            }

            return View(routePoints);
        }

        // GET: RoutePoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoutePoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,Longitude,Lattitude")] RoutePoints routePoints)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routePoints);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routePoints);
        }

        // GET: RoutePoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoints = await _context.RoutePoints.FindAsync(id);
            if (routePoints == null)
            {
                return NotFound();
            }
            return View(routePoints);
        }

        // POST: RoutePoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,Longitude,Lattitude")] RoutePoints routePoints)
        {
            if (id != routePoints.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routePoints);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutePointsExists(routePoints.RouteId))
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
            return View(routePoints);
        }

        // GET: RoutePoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routePoints = await _context.RoutePoints
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (routePoints == null)
            {
                return NotFound();
            }

            return View(routePoints);
        }

        // POST: RoutePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routePoints = await _context.RoutePoints.FindAsync(id);
            _context.RoutePoints.Remove(routePoints);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutePointsExists(int id)
        {
            return _context.RoutePoints.Any(e => e.RouteId == id);
        }
    }
}
