using HealthcareApp.Data;
using HealthcareApp.Models;
using HealthcareApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AppointmentController(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Schedule()
        {
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Schedule(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Check for appointment conflicts
                var conflict = await _context.Appointments
                    .AnyAsync(a => a.DoctorId == appointment.DoctorId && a.AppointmentTime == appointment.AppointmentTime);

                if (conflict)
                {
                    ModelState.AddModelError("", "The selected time slot is already booked.");
                    ViewBag.Patients = _context.Patients.ToList();
                    ViewBag.Doctors = _context.Doctors.ToList();
                    return View(appointment);
                }

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                // Send real-time notification
                var patient = await _context.Patients.FindAsync(appointment.PatientId);
                var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);
                var message = $"New appointment scheduled: {patient.Name} with Dr. {doctor.Name} at {appointment.AppointmentTime}.";
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View(appointment);
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
    }
}
