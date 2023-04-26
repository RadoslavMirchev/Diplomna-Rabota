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
using Beauty_Salon.Services;
using System.Text.Encodings.Web;

namespace Beauty_Salon.Controllers
{
    [Authorize(Roles = "Client,Worker,Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailSender _emailSender;

        public AppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, EmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _context.Appointments.Include(a => a.Procedure).ToList();
            _context.Procedures.Include(p => p.Worker).ToList();
            _context.Appointments.Include(a => a.Client).ToList();
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {

            var appointments = _context.Appointments.Where(x => x.Client.UserName == User.Identity.Name).ToList();
            return View(appointments);
        }

        // GET: Appointments/Details/5
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
        public async Task<IActionResult> Create([Bind("Id,AppointmentDate,AppointmentTime,HourAndMinute,ProcedureId,ClientId")] AppointmentViewModel appointmentView)
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
                Client = await _userManager.FindByNameAsync(User.Identity.Name)
        };
            if (appointment.AppointmentTime.Minute == 0)
            {
                appointment.HourAndMinute += "0";
            }
            var appointments = await _context.Appointments.ToListAsync();
            foreach (var thing in appointments)
            {
                if (thing.AppointmentDate == appointment.AppointmentDate && thing.HourAndMinute == appointment.HourAndMinute)
                {
                    ModelState.AddModelError("AppointmentTime", "This Appointment time is already taken.");
                    return View(appointment);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                await _emailSender.SendEmailAsync(appointment.Client.Email, "Appointment Successfully Created",
                    $"Your Appointment has been successfully created. We will be waiting for you on {appointment.AppointmentDate.Date:dd/MM} at {appointment.HourAndMinute}"
);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppointmentDate,AppointmentTime,HourAndMinute,Duration,ProcedureId,")] AppointmentViewModel appointmentView)
        {
            appointmentView.HourAndMinute = appointmentView.AppointmentTime.Hour + ":" + appointmentView.AppointmentTime.Minute;
            ViewBag.Procedures = _context.Procedures
                     .Select(selector: i => new SelectListItem
                     {
                         Value = i.Id.ToString(),
                         Text = i.Name
                     }).ToList();

            var appointments = await _context.Appointments.ToListAsync();
            foreach (var _appointment in appointments)
            {
                if (_appointment.AppointmentDate == appointmentView.AppointmentDate && _appointment.HourAndMinute == appointmentView.HourAndMinute)
                {
                    ModelState.AddModelError("AppointmentTime", "This Appointment time is already taken.");
                    return View(appointmentView);
                }
            }
            Appointment appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            appointment.AppointmentDate = appointmentView.AppointmentDate.Date;
            appointment.AppointmentTime = appointmentView.AppointmentTime;
            appointment.HourAndMinute = appointmentView.AppointmentTime.Hour + ":" + appointmentView.AppointmentTime.Minute;
            appointment.ProcedureId = appointmentView.ProcedureId;

            if (appointment.AppointmentTime.Minute == 0)
            {
                appointment.HourAndMinute += "0";
            }
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
                    await _emailSender.SendEmailAsync(appointment.Client.Email, "Appointment Successfully Changed",
                    $"Your Appointment has been successfully changed. We will be waiting for you on {appointment.AppointmentDate.Date:dd/MM} at {appointment.HourAndMinute}");
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
