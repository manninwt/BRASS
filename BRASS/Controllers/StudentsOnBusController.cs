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
    public class StudentsOnBusController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsOnBusController(SchoolContext context)
        {
            _context = context;
        }

        // GET: StudentsOnBus
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentsOnBus.ToListAsync());
        }

        // GET: StudentsOnBus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentsOnBus = await _context.StudentsOnBus
                .FirstOrDefaultAsync(m => m.BusId == id);
            if (studentsOnBus == null)
            {
                return NotFound();
            }

            return View(studentsOnBus);
        }

        // GET: StudentsOnBus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentsOnBus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusId,StudentId,FirstName,LastName")] StudentsOnBus studentsOnBus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentsOnBus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentsOnBus);
        }

        // GET: StudentsOnBus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentsOnBus = await _context.StudentsOnBus.FindAsync(id);
            if (studentsOnBus == null)
            {
                return NotFound();
            }
            return View(studentsOnBus);
        }

        // POST: StudentsOnBus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusId,StudentId,FirstName,LastName")] StudentsOnBus studentsOnBus)
        {
            if (id != studentsOnBus.BusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentsOnBus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsOnBusExists(studentsOnBus.BusId))
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
            return View(studentsOnBus);
        }

        // GET: StudentsOnBus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentsOnBus = await _context.StudentsOnBus
                .FirstOrDefaultAsync(m => m.BusId == id);
            if (studentsOnBus == null)
            {
                return NotFound();
            }

            return View(studentsOnBus);
        }

        // POST: StudentsOnBus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentsOnBus = await _context.StudentsOnBus.FindAsync(id);
            _context.StudentsOnBus.Remove(studentsOnBus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsOnBusExists(int id)
        {
            return _context.StudentsOnBus.Any(e => e.BusId == id);
        }
    }
}
