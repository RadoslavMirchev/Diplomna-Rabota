using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Beauty_Salon.Data;
using Beauty_Salon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Beauty_Salon.Controllers
{
    [Authorize(Roles = "Client,Worker,Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _context.Appointments.Include(a => a.Procedure).ToList();
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
              return _context.Appointments != null ? 
                          View(await _context.Appointments.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
        }

        // GET: Appointments/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewBag.Procedures = _context.Procedures
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppointmentDate,AppointmentTime,HourAndMinute,ProcedureId,ProcedureName,WorkerName")] AppointmentViewModel appointmentView)
        {
            ViewBag.Procedures = _context.Procedures
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();
            Appointment appointment = new Appointment
            {
                Id = appointmentView.Id,
                AppointmentDate = appointmentView.AppointmentDate.Date,
                AppointmentTime = appointmentView.AppointmentTime,
                HourAndMinute = appointmentView.AppointmentTime.Hour + ":" + appointmentView.AppointmentTime.Minute,
                ProcedureId = appointmentView.ProcedureId,
                Procedure = _context.Procedures.FirstOrDefault(p => p.Id == appointmentView.ProcedureId),
                ProcedureName = _context.Procedures.Find(appointmentView.ProcedureId).Name,
                WorkerName = _context.Procedures.Find(appointmentView.ProcedureId).WorkerName
            };
            if (appointment.AppointmentTime.Minute == 0)
            {
                appointment.HourAndMinute += "0";
            }
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Procedures = _context.Procedures
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppointmentTime,ProcedureId")] Appointment appointment)
        {
            ViewBag.Procedures = _context.Procedures
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
