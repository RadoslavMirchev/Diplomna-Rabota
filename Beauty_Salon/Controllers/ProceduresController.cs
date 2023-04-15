using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Beauty_Salon.Data;
using Beauty_Salon.Models;

namespace Beauty_Salon.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProceduresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procedures
        public async Task<IActionResult> Index()
        {
              return _context.Procedures != null ? 
                          View(await _context.Procedures.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Procedures'  is null.");
        }

        // GET: Procedures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Procedures == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // GET: Procedures/Create
        public IActionResult Create()
        {
            ViewBag.ApplicationUsers = _context.ApplicationUsers
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.FirstName + " " + i.LastName,
                     }).ToList();
            return View();
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,WorkerId,WorkerName")] ProcedureViewModel procedureView)
        {
            ViewBag.ApplicationUsers = _context.ApplicationUsers
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.FirstName + " " + i.LastName,
                     }).ToList();
            Procedure procedure = new Procedure
            {
                Id = procedureView.Id,
                Name = procedureView.Name,
                Price = procedureView.Price,
                WorkerId = procedureView.WorkerId,
                WorkerName = _context.ApplicationUsers.Find(procedureView.WorkerId).FirstName
            };
            if (ModelState.IsValid)
            {
                _context.Add(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedure);
        }

        // GET: Procedures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Procedures == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Worker")] ProcedureViewModel procedureView)
        {
            if (id != procedureView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedureView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedureExists(procedureView.Id))
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
            return View(procedureView);
        }

        // GET: Procedures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Procedures == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Procedures == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Procedures'  is null.");
            }
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure != null)
            {
                _context.Procedures.Remove(procedure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedureExists(int id)
        {
          return (_context.Procedures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
